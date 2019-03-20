using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Collections.Generic;
using System.Text;

using SW = System.Windows;

namespace WpfApp
{
    static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using (var xamlApp = new XamlApp())
            {
                var wpfApp = new SW.Application();
                var window = new MainWindow()
                {
                    Content = new WindowsXamlHost()
                    {
                        Child = new UwpLib.RootPage(),
                    }
                };
                wpfApp.Run(window);
            }
        }
    }
}
