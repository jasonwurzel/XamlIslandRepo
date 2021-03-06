﻿using System;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using Colors = System.Windows.Media.Colors;
using SW = System.Windows;

namespace ControlPlaygroundXamlIsland
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
                        Child = new ControlPlaygroundUwpLib.RootPage(),
                    }
                };
                wpfApp.Run(window);
            }
        }
    }
}
