using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAutomatoinRestSharp.Utility
{
    using System;
    using System.Collections.Generic;

    public static class AliasUtility
    {
        // Dictionary to store aliases
        private static readonly Dictionary<string, string> AliasStore = new Dictionary<string, string>();

        // Method to store an alias
        public static void StoreAlias(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Key and value cannot be null or empty.");
            }

            AliasStore[key] = value; // Add or update the key-value pair
            Console.WriteLine($"Alias stored: {key} -> {value}");
        }

        // Method to retrieve an alias (optional)
        public static string GetAlias(string key)
        {
            return AliasStore.TryGetValue(key, out var value) ? value : null;
        }
    }
}
