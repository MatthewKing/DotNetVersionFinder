# DotNetVersionFinder

Provides functionality to find the highest version of .NET that is installed on a system. Supports both .NET Framework (4.5 onwards) and newer .NET versions (.NET Core 1-3, and then .NET 5 onwards).

## Quickstart

1) Install the package from [NuGet](https://www.nuget.org/packages/DotNetVersionFinder)
2) Call `DotNetVersion.GetVersion()`
3) You're done!

Alternatively, if you want to be more specific as to which .NET version you're getting, you can use: `DotNetVersion.GetVersionFromDotNetCli()` or `DotNetVersion.GetFrameworkVersionFromRegistry()`. Also, for power users, you can get the release key from the registry using `DotNetVersion.GetFrameworkReleaseKeyFromRegistry()`.

## Supported .NET verions

* .NET via the dotnet CLI. Supports all versions of .NET from 5 onwards.
* .NET Core via the dotnet CLI. Supports all versions of .NET core.
* .NET Framework via the registry. Supports .NET Framework 4.5, 4.5.1, 4.5.2, 4.6, 4.6.1, 4.6.2, 4.7, 4.7.1, 4.7.2, 4.8, 4.8.1.

This library doesn't currently support .NET Framework versions 1.0 through to 4.0, as they are no longer supported by Microsoft, and they add additional complexity to the code. However, if there is enough demand I will gladly add support. Pull requests are welcome.

## Release notes

### 2.0.0

* Overhaul API.
* Add support for .NET 5+
* Add support for .NET Core 1-3
* Add support for .NET Framework 4.8.1

### 1.1.3

* Add support for .NET Framework 4.8.

### 1.1.2

* Add support for undocumented release keys.

### 1.1.0

* Add support for retrieving the current .NET Framework release key via `DotNetVersion.FindReleaseKey()`.

### 1.0.1

* Add support for .NET Framework 4.7.2.

### 1.0.0

* Initial release.

# Installation

Just grab it from [NuGet](https://www.nuget.org/packages/DotNetVersionFinder/)

```
PM> Install-Package DotNetVersionFinder
```

# License and copyright

Copyright (c) Matthew King.
Distributed under the [MIT License](http://opensource.org/licenses/MIT). Refer to license.txt for more information.
