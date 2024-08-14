using TextStreams.Api.Contracts.Dto;
using TextStreams.AppService.Contracts.Interfaces.Commentators;
using TextStreams.AppService.Contracts.Models;
using TextStreams.AppServices.Interfaces;

namespace TextStreams.AppServices.Handlers;

internal class AddMessageHandler : IAddMessageHandler
{
    private readonly IMessagesRepository _messagesRepository;

    public AddMessageHandler(IMessagesRepository messagesRepository)
    {
        _messagesRepository = messagesRepository;
    }

    public async Task Handle(StreamMessageRequest message)
    {
        var messageDto = message.ToDto();
        await _messagesRepository.AddMessage(messageDto);
    }
}