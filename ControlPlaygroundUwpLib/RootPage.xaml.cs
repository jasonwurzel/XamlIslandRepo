using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace UwpLib
{
    public sealed partial class RootPage
    {
        public RootPage()
        {
            InitializeComponent();

            frame.Navigate(typeof(PlaygroundPage));
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string content = args.InvokedItemContainer.Content as string;

            switch (content)
            {
                case "playground":
                    frame.Navigate(typeof(PlaygroundPage));
                    break;
            }
        }
    }
}
