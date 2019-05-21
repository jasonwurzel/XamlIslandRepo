using Microsoft.Toolkit.Win32.UI.XamlHost;

namespace ControlPlaygroundXamlIsland
{
    class XamlApp : XamlApplication
    {
        public XamlApp()
        {
            MetadataProviders.Add(new ControlPlaygroundUwpLib.ControlPlaygroundUwpLib_XamlTypeInfo.XamlMetaDataProvider());
        }
    }
}
