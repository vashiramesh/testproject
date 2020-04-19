using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace ApiTestsSample
{

    [TestClass]
    public class MGPUnitTests
    {

        private static RestClient _client;
	//test code subbu
        public MGPUnitTests()
        {
            _client = new RestClient();
        }

        private TestContext _testContext;

        public TestContext TestContext
        {
            get { return _testContext; }
            set
            {
                _testContext = value;
            }
        }

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        private Uri GetBaseUrl(string method)
        {
            return new Uri($"{TestContext.Properties["ApiUrl"]}{method}");
        }

        [TestInitialize]
        public void SetupTest()
        {
            TestContext.Properties["ApiUrl"] = @"http://mgp-dev.intransgroup.com:88/";
        }

        [TestMethod]
        public void GetVehicleClasses_ReturnsTrue()
        {
            _client.BaseUrl = GetBaseUrl("GetVehicleClasses");

            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = _client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod]
        public void GetAccountType_ReturnsTrue()
        {
            _client.BaseUrl = GetBaseUrl("GetAccountType");

            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");

            IRestResponse response = _client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod]
        public void GetVehicleMakes_ReturnsTrue()
        {
            _client.BaseUrl = GetBaseUrl("GetVehicleMakes");

            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");

            IRestResponse response = _client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod]
        public void UserLogin_ReturnsTrue()
        {
            _client.BaseUrl = GetBaseUrl("UserLogin");

            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "228c952c-d8d1-8062-39de-cdc2f4e72921");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"UserName\": \"ramesh.krishnan@emovis.us\",\r\n  \"Password\": \"Password123\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = _client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod]
        public void MakePayment_ReturnsTrue()
        {
            _client.BaseUrl = GetBaseUrl("UserLogin");

            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "228c952c-d8d1-8062-39de-cdc2f4e72921");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"UserName\": \"ramesh.krishnan@emovis.us\",\r\n  \"Password\": \"Password123\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = _client.Execute(request);


            dynamic summary = JsonConvert.DeserializeObject(response.Content);

            int accountId = summary.Account.AccountId;

            _client.BaseUrl = GetBaseUrl("MakePayment");

            request = new RestRequest(Method.POST);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"AccountId\": " + accountId + ",\r\n  \"CardId\": \"int\",\r\n  \"PaymentAmount\": 2.50,\r\n  \"Card\": {\r\n    \"Address\": {\r\n      \"AddressLine1\": \"11 Main St\",\r\n      \"City\": \"Westbury\",\r\n      \"PostalCode\": \"11590\",\r\n      \"CountryCode\": \"US\",\r\n    },\r\n    \"Name\": \"R K\",\r\n    \"CardNumber\": \"4012001037141112\",\r\n    \"CardType\": 1,\r\n    \"ExpirationMonth\": \"12\",\r\n    \"ExpirationYear\": \"2019\",\r\n    \"SecurityCode\": \"083\"\r\n  }\r\n}", ParameterType.RequestBody);
            response = _client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

    }
}
