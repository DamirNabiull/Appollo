using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Appolo.Helpers
{
    public class PageManager : IDisposable
    {
        public delegate void PageManagerNavigationHandler(object sender, Type typeToNavigate);
        public event PageManagerNavigationHandler PreNavigation;
        public event PageManagerNavigationHandler AfterNavigation;

        public static PageManager Instance { get; private set; }

        public Frame NavigationFrame { get; private set; }

        public delegate void PageManagerRequestHandler(object sender, Type navigatetoType);

        private Dictionary<Type, object> TypeToObjectDictionary { get; set; }

        public IPageManagerTransitionHandler TransitionHandler { get; set; }

        public object CurrentPage { get; private set; }

        public PageManager(Frame navFrame)
        {
            TypeToObjectDictionary = new Dictionary<Type, object>();
            NavigationFrame = navFrame;
            Instance = this;
        }

        public void Navigate(Type navigatetoType)
        {
            ThrowIfDisposed();
            if (CurrentPage != null && CurrentPage.GetType() == navigatetoType)
                return;
            var page = GetPageInstance(navigatetoType);
            _Navigate(page, null);
        }

        public void Navigate(Type navigatetoType, params object[] parameters)
        {
            ThrowIfDisposed();
            if (CurrentPage != null && CurrentPage.GetType() == navigatetoType)
                return;

            var page = GetPageInstance(navigatetoType);
            _Navigate(page, parameters);
        }


        public object GetPageInstance(Type type)
        {
            ThrowIfDisposed();
            if (TypeToObjectDictionary.ContainsKey(type))
                return TypeToObjectDictionary[type];
            else
            {
                var obj = Activator.CreateInstance(type);

                if (obj is IPageManagerHandler)
                {
                    ((IPageManagerHandler)obj).PageManager = this;
                }
                TypeToObjectDictionary.Add(type, obj);
                return obj;
            }
        }


        private void _Navigate(object obj, object[] args)
        {
            var toArgs = new NavigationToArgs
            {
                SourceType = CurrentPage == null ? null : CurrentPage.GetType()
            };

            var fromArgs = new NavigationFromArgs
            {
                TargetType = obj.GetType()
            };

            toArgs.Args = args;
            fromArgs.Args = args;
            Action enterNavigation = () =>
            {
                if (CurrentPage is IPageManagerHandler)
                {
                    var p = (IPageManagerHandler)CurrentPage;
                    p.PreNavigateFrom(fromArgs);
                }
                if (obj is IPageManagerHandler)
                {
                    var p = (IPageManagerHandler)obj;
                    p.PreNavigate(toArgs);
                }
                PreNavigation?.Invoke(this, obj.GetType());
            };
            Action exitNavigation = () =>
            {
                NavigationFrame.Navigate(obj);
                if (obj is IPageManagerHandler)
                {
                    var p = (IPageManagerHandler)obj;
                    p.AfterNavigate(toArgs);
                }
                if (CurrentPage is IPageManagerHandler)
                {
                    var p = (IPageManagerHandler)CurrentPage;
                    p.AfterNavigateFrom(fromArgs);
                }

                AfterNavigation?.Invoke(this, obj.GetType());
                CurrentPage = obj;
            };

            if (TransitionHandler == null)
            {
                enterNavigation();
                exitNavigation();
            }
            else
            {
                TransitionHandler.Translate(enterNavigation, exitNavigation);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected void ThrowIfDisposed()
        {
            if (disposedValue)
                throw new ObjectDisposedException(nameof(PageManager));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var obj in TypeToObjectDictionary)
                    {
                        if (obj.Value is IDisposable)
                        {
                            ((IDisposable)obj.Value).Dispose();
                        }
                    }

                    TypeToObjectDictionary.Clear();
                    TypeToObjectDictionary = null;
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~PageManager()
        // {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class NavigationFromArgs
    {
        public Type TargetType { get; set; }
        public object[] Args { get; set; }
    }


    public class NavigationToArgs
    {
        public Type SourceType { get; set; }
        public object[] Args { get; set; }
    }

    public interface IPageManagerHandler
    {
        PageManager PageManager { get; set; }

        void PreNavigate(NavigationToArgs args);
        void AfterNavigate(NavigationToArgs args);
        void PreNavigateFrom(NavigationFromArgs args);
        void AfterNavigateFrom(NavigationFromArgs args);
    }

    public interface IPageManagerTransitionHandler
    {
        void Translate(Action preNavigation, Action afterNavigation);
    }

    public abstract class PageManagerTransitionHandler : DependencyObject, IPageManagerTransitionHandler
    {
        protected abstract Task EnterAnimationState();
        protected abstract Task ExitAnimationState();

        public void Translate(Action preNavigation, Action afterNavigation)
        {
            Dispatcher.Invoke(async () =>
            {
                preNavigation();
                await EnterAnimationState();
                afterNavigation();
                await ExitAnimationState();
            });
        }
    }

    public class StoryboardTransitionHandler : PageManagerTransitionHandler
    {
        public Storyboard EnterStoryboard { get; set; }
        public Storyboard ExitStoryboard { get; set; }
        private AutoResetEvent _resetEvt;

        public StoryboardTransitionHandler(Storyboard enterSb, Storyboard exitSb)
        {
            EnterStoryboard = enterSb;
            ExitStoryboard = exitSb;

            enterSb.Completed += Sb_Completed;
            exitSb.Completed += Sb_Completed;

            _resetEvt = new AutoResetEvent(false);
        }

        private void Sb_Completed(object sender, EventArgs e)
        {
            _resetEvt.Set();
        }

        protected override Task EnterAnimationState()
        {
            return BeginStoryboard(EnterStoryboard);
        }

        protected override Task ExitAnimationState()
        {
            return BeginStoryboard(ExitStoryboard);
        }

        private async Task BeginStoryboard(Storyboard sb)
        {
            sb.Begin();
            await Task.Run(() => _resetEvt.WaitOne());
        }
    }
}
