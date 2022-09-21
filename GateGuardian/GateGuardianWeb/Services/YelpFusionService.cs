using GateGuardianWeb.Data;
using GateGuardianWeb.Models;
using System.Net.Http.Headers;

public interface IYelpFusionService
{
    public Task<HttpResponseMessage> GetBusinesses(string location);
}

public class YelpFusionService : IYelpFusionService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpClientFactory _httpClientFactory;
    public YelpFusionService(ApplicationDbContext dbContext, IHttpClientFactory httpClientFactory)
    {
        _dbContext = dbContext;
        _httpClientFactory = httpClientFactory;
    }
    public async Task<HttpResponseMessage> GetBusinesses(string location)
    {
        var authorizationToken = _dbContext.Authorizations.FirstOrDefault().Token;
        var uri = $"https://api.yelp.com/v3/businesses/search?location={location}";
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        if (!httpResponseMessage.StatusCode.Equals(200))
        {
            throw new Exception(httpResponseMessage.StatusCode.ToString());
        };
        return httpResponseMessage;
    }
}