using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using TextStreams.Api.Contracts.Dto;

namespace Client.Application;

/// <summary>
/// Клиент для получения стримов.
/// </summary>
public class RestClient
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _url;

    public RestClient(IConfiguration configuration)
    {
        var host = configuration.GetSection("Host").Value;
        if (string.IsNullOrEmpty(host))
            throw new ArgumentException("Host not found in appsettings.json");

        _url = host;
    }

    /// <summary>
    /// Получение всех стримов.
    /// </summary>
    /// <returns> Список стримов.</returns>
    public async Task<List<StreamResponse>> GetStreams(DateOnly date)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_url}/Client/GetStreams", date);

        if (!response.IsSuccessStatusCode)
        {
            return new List<StreamResponse>();
        }

        var groups = await response.Content.ReadFromJsonAsync<List<StreamResponse>>();

        return groups ?? new List<StreamResponse>();
    }
}