using GateGuardianWeb.GraphQL;
using GateGuardianWeb.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace GateGuardianWeb.Controllers
{
    public class YelpGraphQLController : Controller
    {
        private readonly BusinessConsumer _consumer;

        public YelpGraphQLController(BusinessConsumer consumer)
        {
            _consumer = consumer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var businesses = await _consumer.GetAllBusinesses();
            return Ok(businesses);
        }
    }
}
