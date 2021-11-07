using System.Windows;

namespace W2021.WonderfulSalvation
{
    class CellBehavior
    {
        public static readonly DependencyProperty Image1Property =
            DependencyProperty.RegisterAttached("Image1", typeof(string), typeof(CellBehavior), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty Image2Property =
            DependencyProperty.RegisterAttached("Image2", typeof(string), typeof(CellBehavior), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty Image3Property =
            DependencyProperty.RegisterAttached("Image3", typeof(string), typeof(CellBehavior), new FrameworkPropertyMetadata(null));

        public static string GetImage1(DependencyObject d)
        {
            return (string)d.GetValue(Image1Property);
        }
        public static void SetImage1(DependencyObject d, string value)
        {
            d.SetValue(Image1Property, value);
        }

        public static string GetImage2(DependencyObject d)
        {
            return (string)d.GetValue(Image2Property);
        }
        public static void SetImage2(DependencyObject d, string value)
        {
            d.SetValue(Image2Property, value);
        }

        public static string GetImage3(DependencyObject d)
        {
            return (string)d.GetValue(Image3Property);
        }
        public static void SetImage3(DependencyObject d, string value)
        {
            d.SetValue(Image3Property, value);
        }
    }
}
