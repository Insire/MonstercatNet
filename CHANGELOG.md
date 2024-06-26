# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this projects versioning scheme was inspired by [Semantic Versioning](https://semver.org/spec/v2.0.0.html) and can be found [here](versioning.md).

## [3.0.56] - 2024-05-26

### Added

- IUriProvider(s) for overriding Uris for monstercat API and CDN
- HttpClientCookieHandler for pre- and post processing (login) cookie(s)

### Fixed

- TOTP requests and responses

### Removed

- unsupported /signin/token Endpoint
- unsupported /signin/token/resend Endpoint

## [3.0.52] - 2023-12-11

### Added

- .NET 8
- paging support for IMonstercatApi.GetPlaylist

## [3.0.45] - 2023-06-18

### Added

- .NET 7

## [3.0.40] - 2022-08-07

### Added

- cancellation support in MontercatCdnExtensions
- cancellation support in MonstercatApiExtensions

## [3.0.35] - 2022-07-30

### Fixed

- Track.DebutDate not being nullable

## [3.0.32] - 2022-06-26

### Fixed

- UrlBuilderExtension using Artist.Name, instead of Artist.Uri for creating artist photo uri

## [3.0.31] - 2022-06-26

### Fixed

- UrlBuilderExtension.GetSmallArtistPhotoUri fetching images of 1025, instead of 256

### Added

- UrlBuilderExtension.CreateCoverArtUri for Track
- UrlBuilderExtension.GetLargeArtistPhotoUri for TrackArtist
- UrlBuilderExtension.GetSmallArtistPhotoUri for TrackArtist
- UrlBuilderExtension.CreateArtistPhotoUri for TrackArtist

## [3.0.30] - 2022-06-17

### Fixed

- Track
  - Artist -> TrackArtists

### Added

- TrackArtists

## [3.0.28] - 2022-06-12

### Fixed

- Artist
  - ProfileFileId -> nullable

## [3.0.26] - 2022-06-12

### Fixed

- Artist
  - Id -> ArtistId
- Release
  - Brand -> BrandTitle
  - Links -> ReleaseLink

### Added

- ReleaseLink
- Artist
  - ReleaseId
  - ArtistNumber
  - URI
  - ProfileFileId
  - Platform
- Release
  - added AlbumNotes
  - added BrandId
  - added Tags
  - added Artists
  - more misc fields

### Removed

- Track
  - PlaylistSort

## [3.0.17] - 2022-05-15

### Fixed

- symbol package generation

### Added

- LoginValidationHandler, so that its possible to track success of logins
- added endpoint, builder and extensions for fetching artist photos from montercat CDN

## [3.0.9] - 2022-02-27

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
