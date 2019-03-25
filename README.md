# CLI Calculator

Simple command line calculator made while experimenting with F# and FParsec.

The calculator works with both command line arguments and STDIN.

## Build

This project is made with dotnet. To build and run, type:

```
dotnet restore
dotnet run 1+2+3
```

An executable may be built as well with the command:

```
dotnet publish -c Release -r <RID>
```

To see a full list of runtime identifiers (RID), please visit:
https://docs.microsoft.com/en-us/dotnet/core/rid-catalog.
