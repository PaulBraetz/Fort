# Fort #

Fort provides extensions for defensive programming.

## Versioning ##

Fort uses [Semantic Versioning 2.0.0](https://semver.org/).

## Installation ##

Nuget Gallery: https://www.nuget.org/packages/RhoMicro.Fort

Package Manager: `Install-Package RhoMicro.Fort -Version 1.1.0`

.Net CLI: `dotnet add package RhoMicro.Fort --version 1.1.0`

## How To Use ##

Use Fort extensions by including the `Fort` namespace.
```cs
using Fort;

public void MyMethod(String arg){
  arg.ThrowIfDefault(nameof(arg)) //throws ArgumentNullException if arg is null
  
  arg.ThrowIfNot(s => s.StartsWith('G'), $"{nameof(arg)} must start with G.", nameof(arg)) //throws ArgumentException when predicate does not match
}
```
