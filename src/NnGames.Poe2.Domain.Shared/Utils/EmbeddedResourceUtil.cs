using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NnGames.Poe2.Utils
{
    public static class EmbeddedResourceUtil
    {
        public static bool CheckResource(string resourceName, Assembly assembly)
        {
            var names = assembly.GetManifestResourceNames();
            return names.Contains(resourceName);
        }

        public static string ReadResource(string resourceName, Assembly assembly)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName)!)
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}
