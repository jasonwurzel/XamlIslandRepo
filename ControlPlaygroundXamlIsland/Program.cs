using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Color = Windows.UI.Color;
using Colors = System.Windows.Media.Colors;
using Rectangle = Windows.UI.Xaml.Shapes.Rectangle;
using SW = System.Windows;

namespace WpfApp
{
    static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
#if DEBUG
            if (string.IsNullOrEmpty(AppDomain.CurrentDomain.GetData("FX_PRODUCT_VERSION") as string))
                System.Diagnostics.Debug.Fail(".deps.json not deployed");
#endif

            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var xamlApp = new XamlApp())
            {
                var wpfApp = new SW.Application();

                var window = new MainWindow()
                {
                    SnapsToDevicePixels = true,
                    Background = new SW.Media.SolidColorBrush(Colors.OrangeRed),

                    Content = new WindowsXamlHost()
                    {
                        SnapsToDevicePixels = true,
                        Child = new UwpLib.RootPage(),
                    }
                };
                wpfApp.Run(window);
            }
        }
    }
}
