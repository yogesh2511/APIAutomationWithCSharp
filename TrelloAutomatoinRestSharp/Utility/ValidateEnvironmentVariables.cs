using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;

namespace TrelloAutomatoinRestSharp.Utility
{
    public class ValidateEnvironmentVariables
    {
        public static void Validate()
        {
            Env.Load();

            string? apiKey = Environment.GetEnvironmentVariable("TRELLO_API_KEY");
            string? apiToken = Environment.GetEnvironmentVariable("TRELLO_API_TOKEN");

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiToken))
            {
                throw new InvalidOperationException("Environment variables TRELLO_API_KEY or TRELLO_API_TOKEN are not set.");
            }
        }

        public static string GetApiKey()
        {
            Validate();
            return Environment.GetEnvironmentVariable("TRELLO_API_KEY") ?? string.Empty;
        }

        public static string GetApiToken()
        {
            Validate();
            return Environment.GetEnvironmentVariable("TRELLO_API_TOKEN") ?? string.Empty;
        }
    }
}
