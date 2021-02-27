
TruDan's Fork
-------------
My fork contains MonoGame intergration for types such as `Vector3` `Matrix` `Ray` etc, so you won't have to handle the conversions. I also improved the 'Managed' part to make some functions easier to use. I am currently using this library in my project [TruSaber](https://github.com/TruDan/TruSaber).

Oh, also made it into a NuGet Package :)

Installation / Usage
--------------------
Simply install using NuGet. Supports `net5`, `net472`, `netcoreapp3.1`, `netstandard2.1`, `netstandard2.0`

```
dotnet add package SharpVR
```


Original Readme:
----------------

I used these bindings for a school project. I only added bindings for what I needed at the time and I currently have no intention of working on this project.

The bindings themselves are platform agnostic, it's just a layer on top of the generated bindings from OpenVR.
You'll need to copy the `openvr_api.dll` from your SteamVR installation folder so OpenVR can call into it.
