using Refit;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class TrackDownloadRequest
    {
        [AliasAs("format")]
        public FileFormat Format { get; set; } = FileFormat.flac;

        public Guid ReleaseId { get; set; }

        public Guid TrackId { get; set; }

        public TrackDownloadRequest()
        {
            ReleaseId = Guid.Empty;
            TrackId = Guid.Empty;
        }
    }
}
