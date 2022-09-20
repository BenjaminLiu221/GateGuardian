using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace GateGuardianWeb.Controllers
{
    public class YelpController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public YelpController(IHttpClientFactory httpClientFactory)
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
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.yelp.com/v3/businesses/search?location=SanFrancisco");

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "8PLpSqtVhStqvpz6zwj3VwDsry6ZC-6bDISAQtJa6HLdlTtQlB-8dV-0XiRiXjPbKJHvwya6XBvmvk3JE0LHoEIEHMKpSi8Rzr8YS5GdzeBgZT26xDnOE3wWRBQoY3Yx");
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            var json = httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(json);
        }
    }
}
