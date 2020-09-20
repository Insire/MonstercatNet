# MonstercatNet

[![Build status](https://dev.azure.com/SoftThorn/MonstercatNet/_apis/build/status/MonstercatNet-CD)](https://dev.azure.com/SoftThorn/MonstercatNet/_build/latest?definitionId=3)

MonstercatNet is a .NET wrapper around the API that drives [monstercat.com](https://www.monstercat.com/) written in C#.

## Supported Platforms

Since this library relies on [refit](https://github.com/reactiveui/refit) for setting up the API endpoints, their limitations transfer over to this. You can find the limitations [here](https://github.com/reactiveui/refit#where-does-this-work).

## Usage

TODO

## Endpoints

The currently implemented and support endpoints of the monstercat api can be found [here](endpoints.md)

## Download

You can find the MonstercatNet nuget package [here](TODO).

## Versioning

MonstercatNet uses the following versioning strategy:

|number|description|
| - | - |
|major|mirrors the version of the supported monstercat api version|
|minor|major version accroding to [semver](https://semver.org/) (changes when incompatible API changes were made))|
|build|minor version accroding to [semver](https://semver.org/) (changes when functionality in a backwards compatible manner was added|
|revision|patch version accroding to [semver](https://semver.org/) (changes when bugfixes in a backwards compatible manner were made|

----
A special thanks goes out to [defvs](https://github.com/defvs/connect-v2-docs) who with many others documented the unofficial API.
