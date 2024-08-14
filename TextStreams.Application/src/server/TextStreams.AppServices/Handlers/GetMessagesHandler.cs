using TextStreams.AppService.Contracts.Interfaces.Clients;
using TextStreams.AppService.Contracts.Interfaces.Commentators;
using TextStreams.AppServices.Interfaces;

namespace TextStreams.AppServices.Handlers;

internal class GetMessagesHandler : IGetMessagesHandler
{
    private readonly IMessagesRepository _messagesRepository;

    public GetMessagesHandler(IMessagesRepository messagesRepository)
    {
        _messagesRepository = messagesRepository;
    }

    public async Task<IEnumerator<string>> Handle(long request, CancellationToken cancellationToken)
    {
        var messages = await _messagesRepository.GetMessages(request);
        return messages;
    }

    public async Task<IEnumerator<string>> Handle(long streamId)
    {
        var messages = await _messagesRepository.GetMessages(streamId);
        return messages;
    }
}