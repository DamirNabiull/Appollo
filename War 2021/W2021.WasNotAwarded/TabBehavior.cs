using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace W2021.WasNotAwarded
{
    public class TabBehavior
    {
        public static readonly DependencyProperty HeroProperty =
            DependencyProperty.RegisterAttached("Hero",
                                                typeof(Hero),
                                                typeof(TabBehavior),
                                                new FrameworkPropertyMetadata(null));

        public static Hero GetHero(DependencyObject d)
        {
            return (Hero)d.GetValue(HeroProperty);
        }

        public static void SetHero(DependencyObject d, Hero value)
        {
            d.SetValue(HeroProperty, value);
        }
    }
}
