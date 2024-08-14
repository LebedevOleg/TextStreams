using TextStreams.Api.Contracts.Enums;
using TextStreams.AppService.Contracts.Models;
using TextStreams.DataAccess.Entity;
using TextStreams.DataAccess.Repositories;
using Xunit;

namespace TextStreams.UnitTests.TextStreams.DataAccess.Streams;

public class CreateStreamTest
{
    [Fact]
    public void CreateStream_Valid()
    {
        using (var context = new PostgresContext(DataBaseContext.CreateNewContextOptions()))
        {
            var repository = new StreamRepository(context);

            var request = new StreamServiceDto
            {
                TeamHome = "streamTest",
                TeamAway = "Test",
                StartTime = DateTime.UtcNow,
                StartMatchTime = DateTime.UtcNow,
                Status = StreamStatus.Announce,
                GoalsHome = 0,
                GoalsAway = 0
            };

            repository.CreateStream(request, CancellationToken.None).Wait();

            var stream = context.Streams.First(x => x.TeamHome == "streamTest");

            if (stream == null)
                Assert.Fail("Stream not created");
        }
    }
}