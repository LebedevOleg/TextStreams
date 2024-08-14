using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TextStreams.AppServices.Interfaces;
using TextStreams.DataAccess.Entity;
using TextStreams.DataAccess.Repositories;

namespace TextStreams.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PostgresContext>(
            options => options.UseNpgsql(connectionString));

        services.AddScoped<IStreamRepository, StreamRepository>();
        services.AddScoped<IMessagesRepository, MessageRepository>();

        return services;
    }
}