using TextStreams.AppService.Contracts.Models;
using TextStreams.DataAccess.Entity;
using TextStreams.DataAccess.Models;
using TextStreams.DataAccess.Repositories;
using Xunit;

namespace TextStreams.UnitTests.TextStreams.DataAccess.Messages;

public class GetMessagesTest
{
    [Fact]
    public void GetMessages_Valid()
    {
        using (var context = new PostgresContext(DataBaseContext.CreateNewContextOptions()))
        {
            var repository = new MessageRepository(context);

            var stream = new StreamEntity()
            {
                Name = "Test",
                TeamHome = "Test",
                TeamAway = "Test",
                GoalsHome = 0,
                GoalsAway = 0,
                Status = "Announced",
                StartTime = DateTime.UtcNow,
                StartMatchTime = DateTime.UtcNow
            };

            context.Streams.Add(stream);
            context.SaveChanges();

            var message = new StreamMessageEntity()
            {
                StreamId = stream.Id,
                Message = "Test"
            };

            context.StreamMessages.Add(message);
            context.SaveChanges();

            var messages = repository.GetMessages(stream.Id).Result;

            Assert.Single(new[] { messages });
        }
    }
}