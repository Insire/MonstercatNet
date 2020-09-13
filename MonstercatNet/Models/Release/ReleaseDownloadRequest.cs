using Refit;
using SoftThorn.MonstercatNet.Models;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class ReleaseDownloadRequest
    {
        [AliasAs("format")]
        public FileFormat Format { get; set; } = FileFormat.flac;

        public Guid ReleaseId { get; set; }
    }
}
