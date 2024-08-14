using TextStreams.AppService.Contracts.Models;
using TextStreams.DataAccess.Entity;
using TextStreams.DataAccess.Models;
using TextStreams.DataAccess.Repositories;
using Xunit;

namespace TextStreams.UnitTests.TextStreams.DataAccess.Messages;

public class AddMessageTest
{
    [Fact]
    public void Add_Valid()
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

            var message = new MessageServiceDto()
            {
                StreamId = stream.Id,
                Text = "Test"
            };

            repository.AddMessage(message).Wait();

            var messages = context.StreamMessages
                .Where(x => x.StreamId == stream.Id).ToList();
            
            Assert.Single(messages);
        }
    }
}