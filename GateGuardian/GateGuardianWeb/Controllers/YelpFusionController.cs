using GateGuardianWeb.Data;
using GateGuardianWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;

namespace GateGuardianWeb.Controllers
{
    public class YelpFusionController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public YelpFusionController(ApplicationDbContext dbContext, IHttpClientFactory httpClientFactory)
        {
            _dbContext = dbContext;
            _httpClientFactory = httpClientFactory;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpResponseMessage = await GetBusinesses();
            var json = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var jsonDeserialize = JsonConvert.DeserializeObject<Roots>(json);
            Console.WriteLine("");

            Roots roots = new Roots()
            {
                businesses = jsonDeserialize.businesses,
                total = jsonDeserialize.total,
                region = jsonDeserialize.region,
            };
            Console.WriteLine("");
            return View(roots);
        }

        public async Task<HttpResponseMessage> GetBusinesses()
        {
            var authorizationToken = _dbContext.Authorizations.FirstOrDefault().Token;
            var uri = $"https://api.yelp.com/v3/businesses/search?location=SanFrancisco&limit=50";
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            if (!httpResponseMessage.StatusCode.ToString().Equals("OK"))
            {
                throw new Exception("Bad Request.");
            };
            return httpResponseMessage;
        }
    }
}
