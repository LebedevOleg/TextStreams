using TextStreams.Api.Contracts.Dto;

namespace TextStreams.AppService.Contracts.Interfaces.Clients;

/// <summary>
/// Интерфейс получения стримов.
/// </summary>
public interface IGetStreamsHandler : IHandler<DateOnly, IEnumerator<StreamResponse>> 
{
}