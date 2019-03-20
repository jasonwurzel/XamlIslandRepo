using System.Windows;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace XamlIslandPlayground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var navigationView = new NavigationView();
            navigationView.MenuItems.Add(new NavigationViewItem { Content = "one" });
            navigationView.MenuItems.Add(new NavigationViewItem { Content = "two" });
            navigationView.MenuItems.Add(new NavigationViewItem { Content = "three" });

            var theFrame = new Frame();
            navigationView.Content = theFrame;

            new Page1();

            navigationView.ItemInvoked += (sender, args) =>
            {
                var content = args.InvokedItemContainer.Content as string;

                switch (content)
                {
                    case "one":
                        theFrame.Navigate(typeof(Page1));
                        break;
                    case "two":
                        theFrame.Navigate(typeof(Page2));
                        break;
                    case "three":
                        theFrame.Navigate(typeof(Page3));
                        break;
                }
            };

            TheXamlHost.Child = navigationView;
        }
    }

    public class Page1 : Page
    {
        public Page1()
        {
            this.Content = new Rectangle{Fill = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Green), Width = 400, Height = 500};
        }
    }
    public class Page2 : Page
    {
        public Page2()
        {
            this.Content = new Rectangle{Fill = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Red), Width = 400, Height = 500};
        }
    }
    public class Page3 : Page
    {
        public Page3()
        {
            this.Content = new Rectangle{Fill = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Blue), Width = 400, Height = 500};
        }
    }

}
