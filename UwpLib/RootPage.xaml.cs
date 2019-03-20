using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootPage : Page
    {
        public RootPage()
        {
            this.InitializeComponent();

            frame.Navigate(typeof(Page1));
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var content = args.InvokedItemContainer.Content as string;

            switch (content)
            {
                case "one":
                    frame.Navigate(typeof(Page1));
                    break;
                case "two":
                    frame.Navigate(typeof(Page2));
                    break;
                case "three":
                    frame.Navigate(typeof(Page3));
                    break;
            }
        }
    }
}
