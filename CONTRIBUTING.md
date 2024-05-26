# Contribution Guidelines

## Getting started

### Build Requirements

This library uses SDK-style project files, which means you are required to use [Visual Studio 2019](https://visualstudio.microsoft.com/vs/community/) or newer.

- Visual Studio 2019 Community (or better)
- Windows 10 (older versions work probably too, but the repository is not configured for those)
- [.NET Core SDK 6.0](https://dotnet.microsoft.com/download/dotnet-core/6.0)
- [.NET Core SDK 8.0](https://dotnet.microsoft.com/download/dotnet-core/8.0)
- [git](https://git-scm.com/)

(This should be everything, but it's possible i missed something. So please tell me if that's the case.)

### Running tests

The monstercat api has endpoints that are only available to you, if you have an account with an active gold membership. So if you don't have one, then these are expected to **fail**.

### Checking test coverage

You can check coverage locally by running [build.ps1](build.ps1).

```ps1
powershell -ExecutionPolicy ByPass -File build.ps1 -target "Default" -verbosity normal

# the workingdirectory should be the root of this repository for the command to work
```

Which will generate a coverage report that you can open in your browser. The report can be found [here](results/reports/html/index.html) after successfully running the script.

## Coding standards

Please follow the suggestions provided by [.editorconfig](.editorconfig). Otherwise try to follow the patterns provided by preexisting code. I will ask you to update your PR if you are introducing warnings or if you lower test coverage by significant margin.

## Submitting your PR

Probably the smaller the better (within sensible bounds for the nature of your change); at least keep a single feature to a single branch/PR.
