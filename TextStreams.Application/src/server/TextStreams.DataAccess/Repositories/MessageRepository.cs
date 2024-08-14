using TextStreams.AppService.Contracts.Models;
using TextStreams.AppServices.Interfaces;
using TextStreams.DataAccess.Entity;
using TextStreams.DataAccess.Models;

namespace TextStreams.DataAccess.Repositories;

public class MessageRepository : IMessagesRepository
{
    private readonly PostgresContext _context;

    public MessageRepository(PostgresContext context)
    {
        _context = context;
    }

    public Task<IEnumerator<string>> GetMessages(long streamId)
    {
        var messages = _context.StreamMessages
            .Where(x => x.StreamId == streamId)
            .Select(x => x.Message);

        return Task.FromResult(messages.GetEnumerator());
    }

    public async Task AddMessage(MessageServiceDto request)
    {
        var message = request.ToEntity();

        await _context.StreamMessages.AddAsync(message);
        await _context.SaveChangesAsync();
    }
}