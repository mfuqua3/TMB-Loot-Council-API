using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace LootCouncil.Utility.Extensions
{
    public static class AssemblyExtensions
    {
        public static string ReadResource(this Assembly assembly, string resourceName)
        {
            using var stream = assembly.GetResourceStream(resourceName);
            if (stream == null) return default;
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private static Stream GetResourceStream(this Assembly assembly, string resourceName)
        {
            var names = assembly.GetManifestResourceNames();
            var qualifiedResourceName = names.Single(n => n.Contains(resourceName));
            return assembly.GetManifestResourceStream(qualifiedResourceName);
        }

        public static async Task<T> ReadJsonResourceAsync<T>(this Assembly assembly, string resourceName)
        {
            await using var stream = assembly.GetResourceStream(resourceName);
            if (stream == null) return default;
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
}