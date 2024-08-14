using TextStreams.Api.Contracts.Enums;
using TextStreams.DataAccess.Entity;
using TextStreams.DataAccess.Models;
using TextStreams.DataAccess.Repositories;
using Xunit;

namespace TextStreams.UnitTests.TextStreams.DataAccess.Streams;

public class UpdateGoalsStatus
{
    [Fact]
    public void UpdateGoalsStatus_Valid()
    {
        using (var context = new PostgresContext(DataBaseContext.CreateNewContextOptions()))
        {
            var repository = new StreamRepository(context);

            var stream = new StreamEntity()
            {
                Name = "Test",
                TeamHome = "Test",
                TeamAway = "Test",
                GoalsHome = 0,
                GoalsAway = 0,
                Status = StreamStatus.Announce.ToString(),
                StartTime = DateTime.UtcNow,
                StartMatchTime = DateTime.UtcNow
            };

            context.Streams.Add(stream);
            context.SaveChanges();

            repository.UpdateGoals(1, 2, stream.Id, CancellationToken.None).Wait();

            var result = context.Streams.Find(stream.Id);

            if (result == null)
                Assert.Fail("Stream not found");

            Assert.Equal(1, result.GoalsHome);
        }
    }
}