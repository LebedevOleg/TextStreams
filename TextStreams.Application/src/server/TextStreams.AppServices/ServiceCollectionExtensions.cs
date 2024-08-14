
using Microsoft.Extensions.DependencyInjection;
using TextStreams.Api.Contracts.Dto;
using TextStreams.AppService.Contracts.Interfaces;
using TextStreams.AppService.Contracts.Interfaces.Clients;
using TextStreams.AppService.Contracts.Interfaces.Commentators;
using TextStreams.AppServices.Handlers;
using TextStreams.AppServices.Validators;

namespace TextStreams.AppServices;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация обработчиков.
    /// </summary>
    /// <param name="services"> Коллекция сервисов.</param>
    /// <returns> Коллекция сервисов.</returns>
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICreateStreamHandler, CreateStreamHandler>();
        services.AddScoped<IAddMessageHandler, AddMessageHandler>();
        services.AddScoped<IGetMessagesHandler, GetMessagesHandler>();
        services.AddScoped<IUpdateStreamStatusHandler, UpdateStreamStatusHandler>();
        services.AddScoped<IUpdateGoalsHandler, UpdateGoalsHandler>();
        services.AddScoped<IGetStreamsHandler, GetStreamsHandler>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<StreamRequest>, StreamValidator>();
        return services;
    }
}