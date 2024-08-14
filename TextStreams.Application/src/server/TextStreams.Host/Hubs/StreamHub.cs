using Microsoft.AspNetCore.SignalR;
using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Hubs;
using TextStreams.AppService.Contracts.Interfaces.Clients;
using TextStreams.AppService.Contracts.Interfaces.Commentators;

namespace TextStreams.Host.Hubs;

/// <inheritdoc cref = "IStreamHub"/>
public class StreamHub : Hub<IStreamHub>
{
    private readonly IAddMessageHandler _addMessageHandler;
    private readonly IGetMessagesHandler _getMessagesHandler;

    public StreamHub(IAddMessageHandler addMessageHandler, IGetMessagesHandler getMessagesHandler)
    {
        _addMessageHandler = addMessageHandler;
        _getMessagesHandler = getMessagesHandler;
    }

    /// <inheritdoc cref = "IStreamHub.Enter"/>
    public async Task Enter(string groupName, long streamId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        var messages = await _getMessagesHandler.Handle(streamId);
        string streamText = "";
        while (messages.MoveNext())
        {
            streamText += messages.Current + "\n";
        }

        await Clients.Caller.SendMessage(streamText);
    }

    /// <inheritdoc cref = "IStreamHub.SendMessage"/>
    public async Task SendMessage(StreamMessageRequest message)
    {
        await _addMessageHandler.Handle(message);
        await Clients.Group(message.GroupName).SendMessage(message.Text);
    }

    /// <inheritdoc cref = "IStreamHub.Leave"/>
    public async Task Leave(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}