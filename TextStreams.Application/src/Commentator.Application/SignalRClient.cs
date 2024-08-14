using Microsoft.AspNetCore.SignalR.Client;
using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Hubs;

namespace Commentator.Application;

/// <summary>
/// Клиент для SignalR.
/// </summary>
public class SignalRClient
{
    private readonly HubConnection _hubConnection;

    public SignalRClient(string url)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{url}/stream")
            .Build();
    }

    /// <summary>
    /// Подключение к SignalR группе.
    /// </summary>
    /// <param name="currentStream"> Объект стрима.</param>
    public async Task Start(Stream currentStream)
    {
        await _hubConnection.StartAsync();

        _hubConnection.On<string>(nameof(IStreamHub.Enter),
            (message) => { Console.WriteLine($"{message}"); });
        _hubConnection.On<string>(nameof(IStreamHub.SendMessage),
            (message) => { Console.WriteLine($"{message}"); });

        await _hubConnection.SendAsync(nameof(IStreamHub.Enter), currentStream.CurrentGroup);

        Console.WriteLine("Вы успешно вошли в трансляцию");
    }

    /// <summary>
    /// Отключение от SignalR группы.
    /// </summary>
    /// <param name="currentStream"> Объект стрима.</param>
    public async void Leave(Stream currentStream)
    {
        await _hubConnection.SendAsync(nameof(IStreamHub.Leave), currentStream.CurrentGroup);
        await _hubConnection.StopAsync();

        currentStream.CurrentGroup = "";
    }

    /// <summary>
    /// Отправка сообщения.
    /// </summary>
    /// <param name="currentStream"> Объект стрима.</param>
    /// <param name="message"> Сообщение.</param>
    public async void SendMessage(Stream currentStream, string message)
    {
        var request = new StreamMessageRequest
        {
            Text = message,
            GroupName = currentStream.CurrentGroup,
            StreamId = currentStream.CurrentGroupId
        };
        await _hubConnection.SendAsync(nameof(IStreamHub.SendMessage), request);
    }
}