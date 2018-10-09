using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace DotNetVersionFinder
{
    /// <summary>
    /// Provides functionality to find which version of the .NET Framework that is installed on the current machine.
    /// </summary>
    public static class DotNetVersion
    {
        /// <summary>
        /// Maps the release keys to the associated versions.
        /// </summary>
        /// <remarks>
        /// See https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/versions-and-dependencies
        /// </remarks>
        private static readonly SortedDictionary<int, Version> Versions = new SortedDictionary<int, Version>
        {
            [378389] = new Version(4, 5),
            [378675] = new Version(4, 5, 1),
            [378758] = new Version(4, 5, 1),
            [379893] = new Version(4, 5, 2),
            [393295] = new Version(4, 6),
            [393297] = new Version(4, 6),
            [394254] = new Version(4, 6, 1),
            [394271] = new Version(4, 6, 1),
            [394802] = new Version(4, 6, 2),
            [394806] = new Version(4, 6, 2),
            [460798] = new Version(4, 7),
            [460805] = new Version(4, 7),
            [461308] = new Version(4, 7, 1),
            [461310] = new Version(4, 7, 1),
            [461808] = new Version(4, 7, 2),
            [461814] = new Version(4, 7, 2),
        };

        /// <summary>
        /// Returns the .NET Framework version that is installed on this machine.
        /// </summary>
        /// <returns>The .NET Framework version that is installed on this machine.</returns>
        public static Version Find()
        {
            var releaseKey = FindReleaseKey();
            if (releaseKey.HasValue && Versions.TryGetValue(releaseKey.Value, out var version))
            {
                return version;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the release key of the .NET Framework version that is installed on this machine.
        /// </summary>
        /// <returns>The release key of the .NET Framework version that is installed on this machine.</returns>
        public static int? FindReleaseKey()
        {
            return GetDotNetReleaseKeyFromRegistry();
        }

        /// <summary>
        /// Gets the .NET release key from the registry.
        /// </summary>
        /// <returns>The .NET release key, or null if unable to find the release key.</returns>
        /// <remarks>
        /// See https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
        /// </remarks>
        private static int? GetDotNetReleaseKeyFromRegistry()
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                using (var key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"))
                {
                    var value = key.GetValue("Release");
                    if (value is int releaseKey)
                    {
                        return releaseKey;
                    }
                }
            }

            return null;
        }
    }
}
