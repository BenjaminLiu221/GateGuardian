using GateGuardianWeb.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace GateGuardianWeb.Controllers
{
    public class CountryController : Controller
    {
        public class Country
        {
            public string name { get; set; }
        }

        public class CountryResponseType
        {
            [JsonPropertyName("country")]
            public Country Country { get; set; }
        }

        public class CountriesResponseType
        {
            [JsonPropertyName("countries")]
            public List<Country> Countries { get; set; }
        }

        public CountryController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var _client = new GraphQLHttpClient("https://countries.trevorblades.com/", new NewtonsoftJsonSerializer());
            var query = new GraphQLRequest
            {
                Query = @"
                query($id: ID!){
                    country(code:$id) {
                        name
                    }
                }",
                Variables = new
                {
                    id = "CN"
                }
                //Query = @"
                //{
                //    countries {
                //        name
                //    }
                //}"
            };
            var response = await _client.SendQueryAsync<CountryResponseType>(query);
            var countries = response.Data;
            return Ok(countries);
        }
    }
}
