using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using System.Diagnostics;
using System.Net;
using TrelloAutomatoinRestSharp.Base;
using TrelloAutomatoinRestSharp.Utility;

namespace TrelloAutomatoinRestSharp.Boards
{
    public class GetBoardsTest : BaseAPI
    {
        private static IRestClient _client;

        [OneTimeSetUp]
        public static void InitilizeRestClient() =>
            _client = new RestClient("https://api.trello.com");


        private static RestRequest RequestWithAuth(string resource)
        {
            RestRequest request = new RestRequest(resource);

            request.AddParameter("key", ValidateEnvironmentVariables.GetApiKey());
            request.AddParameter("token", ValidateEnvironmentVariables.GetApiToken());
            return request;
        }

        [Test, Order(4)]
        public void CheckGetBoards()
        {
            string listID1= AliasUtility.GetAlias("listID1Board1");
            var request = RequestWithAuth("/1/lists/{list_id}/cards")
                .AddQueryParameter(name:"fields", value:"id,name")
                .AddUrlSegment("list_id", listID1);

            var response = _client.Get(request);
            Console.WriteLine(response.Content.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string boardId = JToken.Parse(response.Content).SelectToken("[1].id").ToString();
            AliasUtility.StoreAlias(key: "board_id", boardId);
            var responseContent = JToken.Parse(response.Content);
           
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));

        }

        [Test, Order(1)]
        public void GetBoardId()
        {
            var request = RequestWithAuth("/1/members/{MemberID}/boards")
               .AddUrlSegment("MemberID", "678cbc268c0cb1b94182fbea");

            var response = _client.Get(request);
            Console.WriteLine(response.Content.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string boardID1 = JToken.Parse(response.Content).SelectToken("[0].id").ToString();
            string boardID2 = JToken.Parse(response.Content).SelectToken("[1].id").ToString();
            string boardID3 = JToken.Parse(response.Content).SelectToken("[2].id").ToString();
            string boardName1 = JToken.Parse(response.Content).SelectToken("[0].name").ToString();
            string boardName2 = JToken.Parse(response.Content).SelectToken("[1].name").ToString();
            string boardName3 = JToken.Parse(response.Content).SelectToken("[2].name").ToString();
            AliasUtility.StoreAlias(key: "boardID1", boardID1);
            AliasUtility.StoreAlias(key: "boardID2", boardID2);
            AliasUtility.StoreAlias(key: "boardID3", boardID3);
            AliasUtility.StoreAlias(key: "boardName1", boardName1);
            AliasUtility.StoreAlias(key: "boardName2", boardName2);
            AliasUtility.StoreAlias(key: "boardName3", boardName3);
      
            var responseContent = JToken.Parse(response.Content);

            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test, Order(2)]
        public void GetCardsoftheListsonBoard()
        {
            string boardId = AliasUtility.GetAlias("boardID2");
            Console.WriteLine(boardId);
            var request = RequestWithAuth("/1/boards/6Me0hT1b/lists")
                .AddQueryParameter(name: "fields", value: "id,name")
                 .AddUrlSegment("BoardID", boardId);

            //var request = RequestWithAuth("/1/boards/6Me0hT1b/lists");

            var response = _client.Get(request);
            Console.WriteLine(response.Content.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string listID1Board1 = JToken.Parse(response.Content).SelectToken("[0].id").ToString();
            AliasUtility.StoreAlias(key: "listID1Board1", listID1Board1);

            var responseContent = JToken.Parse(response.Content);

            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test, Order(3)]
        public void CheckGetASingleBoard()
        {
            string boardId = AliasUtility.GetAlias("boardID2");

            var request = RequestWithAuth("/1/boards/{board_id}")
                 .AddQueryParameter(name: "fields", value: "id,name")
                 .AddUrlSegment("board_id", boardId);

            var response = _client.Get(request);
            Console.WriteLine(response.Content.ToString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_board.json"));
            Assert.True(responseContent.IsValid(jsonSchema));


        }

       

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

    }
}