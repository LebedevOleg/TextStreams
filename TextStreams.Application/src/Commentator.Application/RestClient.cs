using System.Net.Http.Json;
using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Enums;

namespace Commentator.Application;

/// <summary>
/// Клиент для работы с REST API.
/// </summary>
public class RestClient
{
    private readonly HttpClient httpClient = new HttpClient();

    private readonly string _url;

    public RestClient(string url)
    {
        _url = url;
    }

    /// <summary>
    /// Получение всех стримов.
    /// </summary>
    /// <returns> Список стримов.</returns>
    public async Task<List<StreamResponse>> GetStreams(DateOnly date)
    {
        var response = await httpClient.PostAsJsonAsync($"{_url}/Client/GetStreams", date);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            return new List<StreamResponse>();
        }

        var groups = await response.Content.ReadFromJsonAsync<List<StreamResponse>>();

        return groups ?? new List<StreamResponse>();
    }


    /// <summary>
    /// Создание стрима.
    /// </summary>
    /// <param name="request"> Объект стрима.</param>
    public async Task CreateStream(StreamRequest request)
    {
        var response = await httpClient.PostAsJsonAsync($"{_url}/Commentator/CreateStream", request);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Error");
        }
    }

    /// <summary>
    /// Изменение количества голов.
    /// </summary>
    /// <param name="stream"> Объект стрима.</param>
    public async Task ChangeGoal(Stream stream)
    {
        var request = new GoalRequest
        {
            GoalHome = stream.GoalHome,
            GoalAway = stream.GoalAway,
            GroupId = stream.CurrentGroupId
        };

        await httpClient.PutAsJsonAsync($"{_url}/Commentator/UpdateGoals", request);
    }

    /// <summary>
    /// Изменение статуса стрима.
    /// </summary>
    /// <param name="stream"> Объект стрима.</param>
    public async Task ChangeStatus(Stream stream)
    {
        var request = new UpdateStreamStatusDto()
        {
            StreamId = stream.CurrentGroupId,
            Status = stream.Status,
            StartMatchTime = stream.StartTime.ToUniversalTime()
        };

        var resp = await httpClient.PutAsJsonAsync($"{_url}/Commentator/UpdateStreamStatus", request);
    }
}