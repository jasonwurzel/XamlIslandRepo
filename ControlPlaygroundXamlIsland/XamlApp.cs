using Microsoft.Toolkit.Win32.UI.XamlHost;
using WUX = Windows.UI.Xaml;

namespace WpfApp
{
    class XamlApp : XamlApplication
    {
        public XamlApp()
        {
            MetadataProviders.Add(new ControlPlaygroundUwpLib.ControlPlaygroundUwpLib_XamlTypeInfo.XamlMetaDataProvider());
        }
    }
}
