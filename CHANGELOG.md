# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this projects versioning scheme was inspired by [Semantic Versioning](https://semver.org/spec/v2.0.0.html) and can be found [here](versioning.md).

## [3.0.8] - 2022-02-27

### Removed

- MonstercatApi.GetPlaylistTracks (endpoint gone) Tracks are included when fetching a playlist instead
- MonstercatApi.DownloadRelease  (endpoint gone) The web frontend downloads tracks now individually, so thats what i suggest as workaround aswell

### Fixed

- Fetching of covers for releases - available via the new MonstercatCdn class
- MonstercatApi.PlaylistAddTrack
- MonstercatApi.PlaylistDeleteTrack
- MonstercatApi.GetPlaylist
- MonstercatApi.GetSelfPlaylists

### Changed

- cake build scripts to use Cake.Frosting
- Track.Bpm int -> decimal
- ResultBase.Skip -> ResultBase.Offset

## [2.0.32] - 2020-11-08

### Added

- [sourcelink support](https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/sourcelink#using-source-link)

#### Endpoints

- PATCH /playlists

## [2.0.29] - 2020-11-06

### Added

#### Endpoints

- GET /playlist

## [2.0.26] - 2020-10-11

### Added

#### Endpoints

- GET /playlist/{playlistId}/records

## [2.0.25] - 2020-10-10

### Added

#### Endpoints

- DELETE /playlist/{playlistId}/records

## [2.0.24] - 2020-10-10

### Added

#### Endpoints

- PATCH /playlist/{playlistId}/records

## [2.0.22] - 2020-10-04

### Added

#### Endpoints

- GET /self/playlists

## [2.0.21] - 2020-10-04

### Added

#### Endpoints

- POST /self/playlist
- DELETE /playlist/{playlistId}

## [2.0.19] - 2020-08-20

### Added

- license.md

### Changed

- nuget package tags

## [2.0.15] - 2020-08-20

- initial release
