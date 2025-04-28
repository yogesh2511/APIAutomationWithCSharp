using DotNetEnv;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using System;
using System.Diagnostics;
using System.Net;
using TrelloAutomatoinRestSharp.Utility;

namespace TrelloAutomatoinRestSharp
{
    public class BoardsTC 
    {
        private static IRestClient _client;

        [OneTimeSetUp]
        public static void InitilizeRestClient() =>
            _client = new RestClient("https://api.trello.com");

       

public static RestRequest RequestWithAuth(string resource)
        {           
            RestRequest request = new RestRequest(resource);
            request.AddParameter("key", ValidateEnvironmentVariables.GetApiKey());
            request.AddParameter("token", ValidateEnvironmentVariables.GetApiToken());
            return request;
        }
      
        [Test, Order(1)]
        public void GetMembershipsOfABoard()
        {
            var request = RequestWithAuth("/1/members/{MemberID}/boards")
                 .AddQueryParameter(name: "fields", value: "id,name")
                .AddUrlSegment("MemberID", "678cbc268c0cb1b94182fbea");

            var response = _client.Get(request);
            Console.WriteLine(response.Content.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string boardID1= JToken.Parse(response.Content).SelectToken("[0].id").ToString();
            string boardID2 = JToken.Parse(response.Content).SelectToken("[1].id").ToString();
            string boardID3= JToken.Parse(response.Content).SelectToken("[2].id").ToString();
            string boardName1= JToken.Parse(response.Content).SelectToken("[0].name").ToString();
            string boardName2= JToken.Parse(response.Content).SelectToken("[1].name").ToString();
            string boardName3= JToken.Parse(response.Content).SelectToken("[2].name").ToString();
            AliasUtility.StoreAlias(key: "boardID1", boardID1);
            AliasUtility.StoreAlias(key: "boardID2", boardID2);
            AliasUtility.StoreAlias(key: "boardID3", boardID3);
            AliasUtility.StoreAlias(key: "boardName1", boardName1);
            AliasUtility.StoreAlias(key: "boardName2", boardName2);
            AliasUtility.StoreAlias(key: "boardName3", boardName3);
            Console.WriteLine(boardID1);
            Console.WriteLine(boardName1);
            Console.WriteLine(boardID2);
            Console.WriteLine(boardName2);
            Console.WriteLine(boardID3);
            Console.WriteLine(boardName3);
        }

        [Test, Order(2)]
        public void GetABoard()
        {
            string boardID1 = AliasUtility.GetAlias("boardID3");
            Console.WriteLine(boardID1);

            var request = RequestWithAuth("/1/boards/{id}")
                 .AddQueryParameter(name: "fields", value: "id,name")
                .AddUrlSegment("id", boardID1);

            var response = _client.Get(request);
            Console.WriteLine(response.Content.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string boardId = JToken.Parse(response.Content).SelectToken("id").ToString();
           // AliasUtility.StoreAlias(key: "board_id", boardId);
        }

        [Test, Order(2)]
        public void CheckGetBoardWithInvalidID()
        {
          
            var request = RequestWithAuth("/1/boards/{id}")
                            .AddUrlSegment("id", "invalid");

            var response = _client.Get(request);
            Console.WriteLine(response.Content.ToString());
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("invalid id", response.Content);
            // AliasUtility.StoreAlias(key: "board_id", boardId);
        }

        [Test, Order(3)]
        public void GetAFieldOnABoard()
        {
            string boardId = AliasUtility.GetAlias("boardID2");
            string boardName2 = AliasUtility.GetAlias("boardName2");
            Console.WriteLine(boardId);
            Console.WriteLine(boardName2);
            var request = RequestWithAuth("/1/boards/{id}/{field}")
                 .AddQueryParameter(name: "fields", value: "id,name")
                 .AddUrlSegment("id", boardId)
                 .AddUrlSegment("field", "closed");

            //var request = RequestWithAuth("/1/boards/6Me0hT1b/lists");

            var response = _client.Get(request);
            Console.WriteLine(response.Content.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(4)]
        public void GetActionsOfABoard()
        {
            string boardId = AliasUtility.GetAlias("boardID2");
            string boardName2 = AliasUtility.GetAlias("boardName2");
            Console.WriteLine(boardId);
            Console.WriteLine(boardName2);

            var request = RequestWithAuth("/1/boards/{boardId}/actions")
                 .AddQueryParameter(name: "fields", value: "id,name")
                 .AddUrlSegment("boardId", boardId);

            var response = _client.Get(request);
            Console.WriteLine(response.Content.ToString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void TestGetBoardsWithResponseTime()
        {
            // Start measuring time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Create and execute request
            var request = RequestWithAuth("/1/members/{MemberID}/boards")
                .AddUrlSegment("MemberID", "678cbc268c0cb1b94182fbea");

            var response = _client.Get(request);

            // Stop measuring time
            stopwatch.Stop();

            // Validate response
            Console.WriteLine("Response Content: " + response.Content);
            Console.WriteLine("Response Headers: " + string.Join(", ", response.Headers));
            Console.WriteLine("Response Time: " + stopwatch.ElapsedMilliseconds + "ms");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseContent = JToken.Parse(response.Content);

            // Validate JSON Schema
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema), "Response does not match schema.");

            // Validate specific fields
            Assert.IsNotNull(responseContent.SelectToken("[0].id"), "Board ID is null.");
            Assert.IsNotNull(responseContent.SelectToken("[0].name"), "Board Name is null.");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

    }
}