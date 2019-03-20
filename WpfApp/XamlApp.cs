using Microsoft.Toolkit.Win32.UI.XamlHost;
using System;
using System.Collections.Generic;
using System.Text;
using WUX = Windows.UI.Xaml;

namespace WpfApp
{
    class XamlApp : XamlApplication
    {
        public XamlApp()
        {
            MetadataProviders.Add(new UwpLib.UwpLib_XamlTypeInfo.XamlMetaDataProvider());
            //Resources = new WUX.ResourceDictionary { Source = new Uri("ms-appx:///UwpLib/Styles/Styles.xaml") };
        }
    }
}
