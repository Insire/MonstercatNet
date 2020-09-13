using System.IO;

namespace SoftThorn.MonstercatNet.Tests
{
    public static class Extensions
    {
        public static byte[] ToByteArray(this Stream inputStream)
        {
            using (var ms = new MemoryStream())
            {
                var buffer = new byte[16 * 1024];
                int read;

                while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}
