DotNetVersionFinder
===================

Provides functionality to find which version of the .NET Framework is installed on a system.

Quickstart
----------

1) Install the package from [NuGet](https://www.nuget.org/packages/DotNetVersionFinder)
2) Call `DotNetVersion.Find()`
3) You're done!

Supported frameworks
--------------------

The following versions of the .NET Framework are currently supported by this library:

* 4.5
* 4.5.1
* 4.5.2
* 4.6
* 4.6.1
* 4.6.2
* 4.7
* 4.7.1
* 4.7.2
* 4.8

This library doesn't currently support .NET Framework versions 1.0 through to 4.0, as they are no longer supported by Microsoft, and they add additional complexity to the code. However, if there is enough demand I will gladly add support. Pull requests are welcome.

Release notes
-------------

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

Installation
------------

Just grab it from [NuGet](https://www.nuget.org/packages/DotNetVersionFinder/)

```
PM> Install-Package DotNetVersionFinder
```

License and copyright
---------------------

Copyright (c) Matthew King.
Distributed under the [MIT License](http://opensource.org/licenses/MIT). Refer to license.txt for more information.
