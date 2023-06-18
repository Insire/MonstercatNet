using Refit;
using SoftThorn.MonstercatNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MonstercatNet.Sample.Wpf
{
    public static class ReportGeneratorUtility
    {
        public static void Generate()
        {
            var attributes = new List<HttpMethodAttribute>();
            var type = typeof(IMonstercatApi);
            var methods = type.GetMethods();
            var getAttributeType = typeof(GetAttribute);
            var postAttributeType = typeof(PostAttribute);
            var deleteAttributeType = typeof(DeleteAttribute);
            var putAttributeType = typeof(PutAttribute);
            var patchAttributeType = typeof(PatchAttribute);

            foreach (var method in methods)
            {
                attributes.AddRange((HttpMethodAttribute[])Attribute.GetCustomAttributes(method, getAttributeType));
                attributes.AddRange((HttpMethodAttribute[])Attribute.GetCustomAttributes(method, postAttributeType));
                attributes.AddRange((HttpMethodAttribute[])Attribute.GetCustomAttributes(method, deleteAttributeType));
                attributes.AddRange((HttpMethodAttribute[])Attribute.GetCustomAttributes(method, putAttributeType));
                attributes.AddRange((HttpMethodAttribute[])Attribute.GetCustomAttributes(method, patchAttributeType));
            }

            var builder = new StringBuilder();

            var dtos = attributes.OrderBy(p => p.Path).ThenBy(p => p.Method.Method).Select(p => new RenderDto { Method = p.Method.Method, Path = p.Path }).Distinct(new RenderDtoEqualityComparer()).ToList();

            builder.AppendLine("# Endpoints");
            builder.AppendLine();
            builder.AppendLine("|Http method|endpoint|");
            builder.AppendLine("| - | - |");
            foreach (var dto in dtos)
            {
                builder.AppendLine($"|{dto.Method}|{dto.Path}|");
            }

            var result = builder.ToString();
            Debug.WriteLine(result);
            File.WriteAllText(@"E:\Code\Monstercat.Net\endpoints.md", result);
        }

        [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
        private sealed class RenderDto
        {
            public string? Path { get; set; }
            public string? Method { get; set; }

            private string GetDebuggerDisplay()
            {
                return Method + " " + Path;
            }
        }

        private sealed class RenderDtoEqualityComparer : IEqualityComparer<RenderDto>
        {
            public bool Equals(RenderDto? b1, RenderDto? b2)
            {
                if (b2 == null && b1 == null)
                    return true;
                else if (b1 == null || b2 == null)
                    return false;
                else if (b1.Method == b2.Method && b1.Path == b2.Path)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(RenderDto bx)
            {
                var hCode = bx.Method?.GetHashCode() ^ bx.Path?.GetHashCode();
                return hCode.GetHashCode();
            }
        }
    }
}
