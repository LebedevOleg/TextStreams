using TextStreams.Api.Contracts.Enums;
using TextStreams.DataAccess.Entity;
using TextStreams.DataAccess.Models;
using TextStreams.DataAccess.Repositories;
using Xunit;

namespace TextStreams.UnitTests.TextStreams.DataAccess.Streams;

public class GetStreamsTests
{
    [Fact]
    public void GetStreams_None_Valid()
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

            var result = repository.GetStreams(
                    DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                    CancellationToken.None)
                .Result;

            Assert.Empty(result);
        }
    }

    [Fact]
    public void GetStreams_NotNone_Valid()
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

            var result = repository.GetStreams(
                    DateOnly.FromDateTime(DateTime.Now),
                    CancellationToken.None)
                .Result;

            Assert.NotEmpty(result);
        }
    }
}