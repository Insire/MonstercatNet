using System.Runtime.Serialization;

namespace SoftThorn.MonstercatNet.Models
{
    public enum FileFormat
    {
        // order is personal preference

        [EnumMember(Value = "flac")]
        flac = 1,

        [EnumMember(Value = "mp3_320")]
        mp3 = 2,

        [EnumMember(Value = "wav")]
        wav = 3
    }
}
