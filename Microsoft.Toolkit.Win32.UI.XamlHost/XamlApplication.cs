// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using windows = Windows;

namespace Microsoft.Toolkit.Win32.UI.XamlHost
{
    /// <summary>
    /// XamlApplication is a custom <see cref="windows.UI.Xaml.Application" /> that implements <see cref="windows.UI.Xaml.Markup.IXamlMetadataProvider" />. The
    /// metadata provider implemented on the application is known as the 'root metadata provider'.  This provider
    /// has the responsibility of loading all other metadata for custom UWP XAML types.  In this implementation,
    /// reflection is used at runtime to probe for metadata providers in the working directory, allowing any
    /// type that includes metadata (compiled in to a .NET framework assembly) to be used without explicit
    /// metadata handling by the developer.
    /// </summary>
    public class XamlApplication : windows.UI.Xaml.Application, windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
        public static event EventHandler ApplicationCreated;

        private static readonly List<Type> FilteredTypes = new List<Type>
        {
            typeof(XamlApplication),
            typeof(windows.UI.Xaml.Markup.IXamlMetadataProvider)
        };

        // Metadata provider identified by the root metadata provider
        private List<windows.UI.Xaml.Markup.IXamlMetadataProvider> _metadataProviders = null;

        public void LoadResources(Uri uri)
        {
            Resources = new windows.UI.Xaml.ResourceDictionary { Source = uri };
        }

        /// <summary>
        /// Gets XAML <see cref="windows.UI.Xaml.Markup.IXamlType"/> interface from all cached metadata providers for the <paramref name="type"/>.
        /// </summary>
        /// <param name="type">Type of requested type</param>
        /// <returns>IXamlType interface or null if type is not found</returns>
        windows.UI.Xaml.Markup.IXamlType windows.UI.Xaml.Markup.IXamlMetadataProvider.GetXamlType(Type type)
        {
            EnsureMetadataProviders();

            foreach (var provider in _metadataProviders)
            {
                var result = provider.GetXamlType(type);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets XAML IXamlType interface from all cached metadata providers by full type name
        /// </summary>
        /// <param name="fullName">Full name of requested type</param>
        /// <returns><see cref="windows.UI.Xaml.Markup.IXamlType"/> if found; otherwise, null.</returns>
        windows.UI.Xaml.Markup.IXamlType windows.UI.Xaml.Markup.IXamlMetadataProvider.GetXamlType(string fullName)
        {
            EnsureMetadataProviders();

            foreach (var provider in _metadataProviders)
            {
                var result = provider.GetXamlType(fullName);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all XAML namespace definitions from metadata providers
        /// </summary>
        /// <returns>Array of namespace definitions</returns>
        windows.UI.Xaml.Markup.XmlnsDefinition[] windows.UI.Xaml.Markup.IXamlMetadataProvider.GetXmlnsDefinitions()
        {
            EnsureMetadataProviders();

            var definitions = new List<windows.UI.Xaml.Markup.XmlnsDefinition>();
            foreach (var provider in _metadataProviders)
            {
                definitions.AddRange(provider.GetXmlnsDefinitions());
            }

            return definitions.ToArray();
        }

        /// <summary>
        /// Probes file system for UWP XAML metadata providers
        /// </summary>
        private void EnsureMetadataProviders()
        {
            if (_metadataProviders == null)
            {
                _metadataProviders = MetadataProviderDiscovery.DiscoverMetadataProviders(FilteredTypes);
            }
        }

        private static readonly Action<XamlApplication> Noop = _ => { };

        /// <summary>
        /// Gets and returns the current UWP XAML Application instance in a reference parameter.
        /// If the current XAML Application instance has not been created for the process (is null),
        /// a new <see cref="Microsoft.Toolkit.Win32.UI.XamlHost.XamlApplication" /> instance is created and returned.
        /// </summary>
        /// <returns>A delegate that shall be executed when the thread is fully initialized.</returns>
        internal static Action<XamlApplication> GetOrCreateXamlApplicationInstance(ref windows.UI.Xaml.Application application)
        {
            // Instantiation of the application object must occur before creating the DesktopWindowXamlSource instance.
            // DesktopWindowXamlSource will create a generic Application object unable to load custom UWP XAML metadata.
            if (application == null)
            {
                try
                {
                    // windows.UI.Xaml.Application.Current may throw if DXamlCore has not been initialized.
                    // Treat the exception as an uninitialized windows.UI.Xaml.Application condition.
                    application = windows.UI.Xaml.Application.Current;
                }
                catch
                {
                    // Create a custom UWP XAML Application object that implements reflection-based XAML metadata probing.
                    application = new XamlApplication();
                    return RaiseApplicationCreated;
                }
            }

            return Noop;
        }

        private static void RaiseApplicationCreated(XamlApplication application)
        {
            ApplicationCreated?.Invoke(application, new EventArgs());
        }
    }
}
