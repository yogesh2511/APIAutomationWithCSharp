using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;
using TrelloAutomatoinRestSharp.Utility;

namespace TrelloAutomatoinRestSharp.Base
{
    public class BaseAPI
    {
        // Base API URL and authentication details
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApiToken { get; set; }

        // Constructor to initialize BaseAPI
        public BaseAPI()
        {
            BaseUrl = ResourceReader.GetResourceValue("BaseURl");
            ApiKey = ValidateEnvironmentVariables.GetApiKey();
            ApiToken = ValidateEnvironmentVariables.GetApiToken();
        }

        // Method to construct the full URL
        public string GetFullUrl(string endpoint)
        {
            return $"{BaseUrl}/{endpoint}?key={ApiKey}&token={ApiToken}";
        }
    }
}
