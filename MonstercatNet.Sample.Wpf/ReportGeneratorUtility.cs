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

            var dtos = attributes.OrderBy(p => p.Method.Method).ThenBy(p => p.Path).Select(p => new RenderDto { Method = p.Method.Method, Path = p.Path }).ToList();

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
        private class RenderDto
        {
            public string Path { get; set; }
            public string Method { get; set; }

            private string GetDebuggerDisplay()
            {
                return Method + " " + Path;
            }
        }
    }
}
