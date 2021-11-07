using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using W2021.Helpers;

namespace W2021.WonderfulSalvation
{
    public partial class MainWindow : Window
    {
        public MainWindow(int index)
        {
            InitializeComponent();

            NavFrame.Navigate(index == 0 ? new PageL() : new PageR());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            this.KeyDown += MainWindow_KeyDown;

            Canvas.SetLeft(Box, Config<AppConfig>.Value.Left);
            Canvas.SetTop(Box, Config<AppConfig>.Value.Top);
            Box.Width = Config<AppConfig>.Value.Width;
            Box.Height = Config<AppConfig>.Value.Height;
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var value = Keyboard.IsKeyDown(Key.LeftCtrl) ? -1 : 1;
            value = Keyboard.IsKeyDown(Key.RightCtrl) ? value * 10 : value;

            if (e.Key == Key.Left)
            {
                Config<AppConfig>.Value.Left += value;
                Canvas.SetLeft(Box, Config<AppConfig>.Value.Left);
            }
            else if (e.Key == Key.Right)
            {
                Config<AppConfig>.Value.Width -= value;
                Box.Width = Config<AppConfig>.Value.Width;
            }
            else if (e.Key == Key.Up)
            {
                Config<AppConfig>.Value.Top += value;
                Canvas.SetTop(Box, Config<AppConfig>.Value.Top);
            }
            else if (e.Key == Key.Down)
            {
                Config<AppConfig>.Value.Height -= value;
                Box.Height = Config<AppConfig>.Value.Height;
            } else if (e.Key == Key.S)
            {
                Config<AppConfig>.Save();
            }
        }
    }
}
