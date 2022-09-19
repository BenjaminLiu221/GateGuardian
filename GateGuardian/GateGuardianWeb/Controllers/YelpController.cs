using Microsoft.AspNetCore.Mvc;

namespace GateGuardianWeb.Controllers
{
    public class YelpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("name = Businesses")]
        public async IActionResult GetAllBusinesses()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.yelp.com/v3/businesses/search?location=SanFrancisco");

        }
    }
}
