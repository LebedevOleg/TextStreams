using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using TextStreams.Api.Contracts.Hubs;

namespace Client.Application;

/// <summary>
/// Клиент для SignalR.
/// </summary>
public class SignalRClient
{
    private readonly HubConnection _hubConnection;

    private string _groupName = "";

    public SignalRClient(IConfiguration configuration)
    {
        var host = configuration.GetSection("Host").Value;
        if (string.IsNullOrEmpty(host))
            throw new ArgumentException("Host not found in appsettings.json");

        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{host}/stream")
            .Build();
        ;
    }

    /// <summary>
    /// Статус подключения.
    /// </summary>
    public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;

    /// <summary>
    /// Подключение к SignalR группе.
    /// </summary>
    /// <param name="groupName">Имя группы.</param>
    /// <param name="id">Идентификатор трансляции.</param>
    public async Task Start(string groupName, long id)
    {
        await _hubConnection.StartAsync();

        _groupName = groupName;

        await _hubConnection.SendAsync(nameof(IStreamHub.Enter), groupName, id);
        _hubConnection.On<string>(nameof(IStreamHub.Enter),
            (message) => { Task.Run(() => { Console.WriteLine($"{message}"); }); });
        _hubConnection.On<string>(nameof(IStreamHub.SendMessage),
            (message) => { Task.Run(() => { Console.WriteLine($"{message}"); }); });

        Console.WriteLine("Вы успешно вошли в трансляцию");
    }

    /// <summary>
    /// Отключение от SignalR группы.
    /// </summary>
    public async void Leave()
    {
        await _hubConnection.SendAsync(nameof(IStreamHub.Leave), _groupName);
        await _hubConnection.StopAsync();

        _groupName = "";
    }
}