# MonstercatNet

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/Insire/MonstercatNet/blob/master/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/v/MonstercatNet)](https://www.nuget.org/packages/MonstercatNet/)
[![Build status](https://dev.azure.com/SoftThorn/MonstercatNet/_apis/build/status/MonstercatNet-CD)](https://dev.azure.com/SoftThorn/MonstercatNet/_build/latest?definitionId=3)
[![CodeFactor](https://www.codefactor.io/repository/github/insire/monstercatnet/badge)](https://www.codefactor.io/repository/github/insire/monstercatnet)
[![codecov](https://codecov.io/gh/Insire/MonstercatNet/branch/master/graph/badge.svg)](https://codecov.io/gh/Insire/MonstercatNet)

MonstercatNet is a .NET wrapper around the API that drives [monstercat.com](https://www.monstercat.com/) written in C#.

## Supported Platforms

Since this library relies on [refit](https://github.com/reactiveui/refit) for setting up the API endpoints, there are limitations. You can find the limitations [here](https://github.com/reactiveui/refit#where-does-this-work).

## Installation

You can find the latest nuget package [here](https://www.nuget.org/packages/MonstercatNet/).

## Changelog

See [here](CHANGELOG.md)

## Usage

### creating the api client

```cs
using SoftThorn.MonstercatNet;

var httpClient  = new HttpClient().UseMonstercatApiV2();
var client = MonstercatApi.Create(httpClient);
```

### creating the cdn client

```cs
using SoftThorn.MonstercatNet;

var httpClient  = new HttpClient().UseMonstercatCdn();
var client = MonstercatCdn.Create(httpClient);
```

**NOTE:** The ``HttpClient`` for the CDN should be a different instance than the one for the API, as they use a different BaseAddress.

### signing in

```cs
using SoftThorn.MonstercatNet;

var credentials = new ApiCredentials()
{
    Email = "", // your account e-mail
    Password = "" // your password
};

await client.Login(credentials);
```

### searching for music

```cs
using SoftThorn.MonstercatNet;

var tracks = await client.SearchTracks(new TrackSearchRequest()
{
    Limit = 1,
    Skip = 0,
    Creatorfriendly = true,
    Genres = new[] { "Drumstep" },
    ReleaseTypes = new[] { "Album" },
    Tags = new[] { "Uncaged", "Energetic" },
});
```

### getting albumns, EPs and singles (releases)

```cs
using SoftThorn.MonstercatNet;

var releases = await client.GetReleases(new ReleaseBrowseRequest()
{
    Limit = 1,
    Skip = 0
});
```

### getting release details

```cs
using SoftThorn.MonstercatNet;

var release = await client.GetRelease("the release catalogId which looks like this MCRLX001-8");
```

### getting release cover

```cs
using SoftThorn.MonstercatNet;

var cdn = MonstercatCdn.Create(new HttpClient(new HttpLoggingHandler()).UseMonstercatCdn());

var builder = ReleaseCoverArtBuilder
    .Create()
    .ForRelease(new TrackRelease() { CatalogId = "the release catalog Id which looks like this 2FMCS1347" };);

var releaseCoverBytes = await cdn.GetReleaseCoverAsByteArray(builder);
var releaseCoverStream = await cdn.GetReleaseCoverAsStream(builder);
```

## Endpoints

The currently implemented and supported endpoints can be found [here](endpoints.md)

## Versions

See [here](versioning.md)

## Contributing

See [here](CONTRIBUTING.md)

----
A special thanks goes out to [defvs](https://github.com/defvs/connect-v2-docs) who with many others documented the unofficial API.
