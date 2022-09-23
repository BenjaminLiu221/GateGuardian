using GateGuardianWeb.Config;
using GateGuardianWeb.Models.Passwords;
using Microsoft.Extensions.Options;
using System;

public interface IPasswordsService
{
    public Task<List<Password>> GetAllPasswords();
}

public class PasswordsService: IPasswordsService
{
    private readonly HttpClient _httpClient;
    private readonly PasswordsApiOptions _apiConfig;

    public PasswordsService(HttpClient httpClient, IOptions<PasswordsApiOptions> apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig.Value;
    }

    public async Task<List<Password>> GetAllPasswords()
    {
        var passwordsResponse = await _httpClient
            .GetAsync("https://foo.com");

        if (passwordsResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<Password> { };
        }
        var responseContent = passwordsResponse.Content;
        var allPasswords = await responseContent.ReadFromJsonAsync<List<Password>>();
        return allPasswords.ToList();
    }
}