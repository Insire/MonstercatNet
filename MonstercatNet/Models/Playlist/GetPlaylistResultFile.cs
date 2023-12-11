using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class GetPlaylistResultFile
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Filepath { get; set; } = string.Empty;

        public string Filename { get; set; } = string.Empty;

        public string MimeType { get; set; } = string.Empty;

        public bool Deleted { get; set; }

        public Guid LastUser { get; set; }

        public string LastAction { get; set; } = string.Empty;

        public string LastStatus { get; set; } = string.Empty;

        public string LastMessage { get; set; } = string.Empty;

        public object Statuses { get; set; } = string.Empty;
    }
}
