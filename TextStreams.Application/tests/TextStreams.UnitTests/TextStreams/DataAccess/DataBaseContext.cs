using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TextStreams.DataAccess.Entity;

namespace TextStreams.UnitTests.TextStreams.DataAccess;

public class DataBaseContext
{
    public static DbContextOptions<PostgresContext> CreateNewContextOptions()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var builder = new DbContextOptionsBuilder<PostgresContext>();
        builder.UseInMemoryDatabase("TextStreams")
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }
}