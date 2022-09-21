//using GateGuardianWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Http.Headers;

namespace GateGuardianWeb.Controllers
{
    public class YelpFusionController : Controller
    {
        public class Business
        {
            public string id { get; set; }
            public string alias { get; set; }
            public string name { get; set; }
            public string image_url { get; set; }
            public bool is_closed { get; set; }
            public string url { get; set; }
            public int review_count { get; set; }
            public List<Category> categories { get; set; }
            public double rating { get; set; }
            public Coordinates coordinates { get; set; }
            public List<string> transactions { get; set; }
            public string price { get; set; }
            public Location location { get; set; }
            public string phone { get; set; }
            public string display_phone { get; set; }
            public double distance { get; set; }
        }

        public class Category
        {
            public string alias { get; set; }
            public string title { get; set; }
        }

        public class Center
        {
            public double longitude { get; set; }
            public double latitude { get; set; }
        }

        public class Coordinates
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class Location
        {
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string address3 { get; set; }
            public string city { get; set; }
            public string zip_code { get; set; }
            public string country { get; set; }
            public string state { get; set; }
            public List<string> display_address { get; set; }
        }

        public class Region
        {
            public Center center { get; set; }
        }

        public class Root
        {
            public List<Business> businesses { get; set; }
            public int total { get; set; }
            public Region region { get; set; }
        }



        private readonly IHttpClientFactory _httpClientFactory;

        public YelpFusionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task GetAllBusinesses()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.yelp.com/v3/businesses/search?location=SanFrancisco&limit=200");

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "8PLpSqtVhStqvpz6zwj3VwDsry6ZC-6bDISAQtJa6HLdlTtQlB-8dV-0XiRiXjPbKJHvwya6XBvmvk3JE0LHoEIEHMKpSi8Rzr8YS5GdzeBgZT26xDnOE3wWRBQoY3Yx");
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            var json = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var jsonDeserialize = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("");

        }
    }
}
