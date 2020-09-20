using Refit;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class ReleaseDownloadRequest
    {
        [AliasAs("format")]
        public FileFormat Format { get; set; } = FileFormat.flac;

        public Guid ReleaseId { get; set; }

        public ReleaseDownloadRequest()
        {
            ReleaseId = Guid.Empty;
        }
    }
}
