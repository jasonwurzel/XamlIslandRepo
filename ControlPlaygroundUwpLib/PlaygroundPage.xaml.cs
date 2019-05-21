using System;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace UwpLib
{
    public sealed partial class PlaygroundPage
    {
        public PlaygroundPage()
        {
            InitializeComponent();

            TheListView.ItemsSource = Enumerable.Range(0,15);
        }

        private void TheListView2_OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }
    }
}
