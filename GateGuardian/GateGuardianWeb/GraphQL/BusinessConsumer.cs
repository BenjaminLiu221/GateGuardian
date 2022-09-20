using GateGuardianWeb.Models;
using GateGuardianWeb.ResponseTypes;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System.Net.Http.Headers;

namespace GateGuardianWeb.GraphQL
{
    public class BusinessConsumer
    {
        //private readonly GraphQLHttpClient _client;

        //public BusinessConsumer(GraphQLHttpClient client)
        //{
        //    _client = client;
        //}

        public async Task<List<Business>> GetAllBusinesses()
        {
            var _client = new GraphQLHttpClient("https://api.yelp.com/v3/graphql", new NewtonsoftJsonSerializer());
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "8PLpSqtVhStqvpz6zwj3VwDsry6ZC-6bDISAQtJa6HLdlTtQlB-8dV-0XiRiXjPbKJHvwya6XBvmvk3JE0LHoEIEHMKpSi8Rzr8YS5GdzeBgZT26xDnOE3wWRBQoY3Yx");
            var query = new GraphQLRequest
            {
                Query = @"
                        query businessesQuery{
                          business{
                            id
                            name
                          }
                        }"
            };
            var response = await _client.SendQueryAsync<ResponseBusinessCollectionType>(query);
            return response.Data.Businesses;
        }
    }
}
