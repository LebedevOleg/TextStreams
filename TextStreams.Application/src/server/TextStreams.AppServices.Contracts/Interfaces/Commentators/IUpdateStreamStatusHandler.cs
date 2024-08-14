using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Enums;

namespace TextStreams.AppService.Contracts.Interfaces.Commentators;

/// <summary>
/// Интерфейс обновления статуса стрима.
/// </summary>
public interface IUpdateStreamStatusHandler : IHandler<UpdateStreamStatusDto>
{
    
}