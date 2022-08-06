# Fort #

Fort provides extensions for defensive programming.

## Versioning ##

Fort uses [Semantic Versioning 2.0.0](https://semver.org/).

## Installation ##



## How To Use ##

Use Fort extensions by including the `Fort` namespace.
```cs
using Fort;

public void MyMethod(String arg){
  arg.ThrowIfDefault(nameof(arg)) //throws ArgumentNullException if arg is null
  
  arg.ThrowIfNot(s => s.StartsWith('G'), $"{nameof(arg)} must start with G.", nameof(arg)) //throws ArgumentException when predicate does not match
}
```
