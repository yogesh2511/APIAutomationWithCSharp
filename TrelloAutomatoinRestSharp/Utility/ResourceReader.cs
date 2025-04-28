using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAutomatoinRestSharp.Utility
{
    using System.Resources;

    public static class ResourceReader
    {
        public static string GetResourceValue(string key)
        {
            // Access the resource manager for TestData
            var resourceManager = new ResourceManager("TrelloAutomatoinRestSharp.Resources.TestData", typeof(ResourceReader).Assembly);

            // Retrieve the value for the given key
            return resourceManager.GetString(key) ?? throw new KeyNotFoundException($"Key '{key}' not found in TestData resource file.");
        }
    }

}
