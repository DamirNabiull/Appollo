using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace W2021.SpringOfVictory
{
    class MultiplyEffect: ShaderEffect
    {
        private static PixelShader _pixelShader = new PixelShader();
        
        static MultiplyEffect()
        {
            _pixelShader.UriSource = new Uri("MultiplyEffect.ps", UriKind.Relative);
        }

        public MultiplyEffect()
        {
            PixelShader = _pixelShader;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(OverlayProperty);
        }

        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(MultiplyEffect), 0);

        public Brush Overlay
        {
            get { return (Brush)GetValue(OverlayProperty);}
            set { SetValue(OverlayProperty, value); }
        }

        public static readonly DependencyProperty OverlayProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Overlay", typeof(MultiplyEffect), 1);
    }
}
