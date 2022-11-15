using System.Diagnostics;
using Microsoft.Win32;

namespace DotNetVersionFinder;

/// <summary>
/// Provides functionality to find the version of .NET that is installed on the current machine.
/// </summary>
public static class DotNetVersion
{
    /// <summary>
    /// Returns the highest version of .NET that is installed on this machine.
    /// This will look for the version information from the dotnet CLI and then
    /// fall back to the .NET Framework version from the registry.
    /// </summary>
    /// <returns>The .NET version (or null if it was unable to be determined).</returns>
    public static Version GetVersion()
    {
        return GetDotNetCliVersion() ?? GetFrameworkVersion();
    }

    /// <summary>
    /// Returns the version of .NET that is installed on this machine, as reported by the dotnet CLI.
    /// </summary>
    /// <returns>The .NET version (or null if it was unable to be determined).</returns>
    public static Version GetDotNetCliVersion()
    {
        try
        {
            var psi = new ProcessStartInfo();
            psi.FileName = "dotnet";
            psi.Arguments = "--version";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;

            using var process = Process.Start(psi);

            if (process is not null)
            {
                process.WaitForExit();

                if (Version.TryParse(process.StandardOutput.ReadToEnd(), out var version))
                {
                    return version;
                }
            }
        }
        catch (Exception)
        {
            // Swallow all exceptions.
        }

        return null;
    }

    /// <summary>
    /// Returns the highest known version of the .NET Framework that is installed on this machine,
    /// determined by reading the framework release key from the Windows registry.
    /// </summary>
    /// <returns>The .NET Framework version (or null if it was unable to be determined).</returns>
    public static Version GetFrameworkVersion()
    {
        // See https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/minimum-release-dword

        return GetFrameworkReleaseKey() switch
        {
            >= 533320 => new Version(4, 8, 1),
            >= 528040 => new Version(4, 8),
            >= 461808 => new Version(4, 7, 2),
            >= 461308 => new Version(4, 7, 1),
            >= 460798 => new Version(4, 7),
            >= 394802 => new Version(4, 6, 2),
            >= 394254 => new Version(4, 6, 1),
            >= 393295 => new Version(4, 6),
            >= 379893 => new Version(4, 5, 2),
            >= 378675 => new Version(4, 5, 1),
            >= 378389 => new Version(4, 5),
            _ => null,
        };
    }

    /// <summary>
    /// Returns the .NET Framework release key (from the Windows registry).
    /// </summary>
    /// <returns>The .NET Framework release key, or null if unable to find the release key.</returns>
    /// <remarks>
    /// See https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
    /// </remarks>
    public static int? GetFrameworkReleaseKey()
    {
#if NET6_0_OR_GREATER
        if (!OperatingSystem.IsWindows())
        {
            return null;
        }
#endif
        try
        {
            using var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            using var key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\");
            var value = key.GetValue("Release");
            if (value is int releaseKey)
            {
                return releaseKey;
            }
        }
        catch (Exception)
        {
            // Swallow all exceptions.
        }

        return null;
    }
}
