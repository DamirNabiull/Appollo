using System.Windows;

namespace W2021.SpringOfVictory
{
    public class CellBehavior
    {
        public static readonly DependencyProperty HeroProperty =
            DependencyProperty.RegisterAttached("Hero",
                                                typeof(Hero),
                                                typeof(CellBehavior),
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
