using TextStreams.Api.Contracts.Enums;
using TextStreams.AppService.Contracts.Models;

namespace TextStreams.DataAccess.Models;

public static class StreamEntityMappingExtensions
{
    /// <summary>
    /// Преобразование DTO в сущность.
    /// </summary>
    /// <param name="request"> DTO с данными стрима.</param>
    /// <returns> Сущность стрима.</returns>
    public static StreamEntity ToEntity(this StreamServiceDto request)
    {
        return new StreamEntity
        {
            Name = request.Name,
            TeamHome = request.TeamHome,
            TeamAway = request.TeamAway,
            GoalsHome = request.GoalsHome,
            GoalsAway = request.GoalsAway,
            StartTime = request.StartTime,
            Status = request.Status.ToString(),
            StartMatchTime = request.StartMatchTime,
        };
    }

    /// <summary>
    /// Преобразование сущности в DTO.
    /// </summary>
    /// <param name="entity"> Сущность стрима.</param>
    /// <returns> DTO с данными стрима.</returns>
    public static StreamServiceDto ToDto(this StreamEntity entity)
    {
        return new StreamServiceDto
        {
            Id = entity.Id,
            Name = entity.Name,
            TeamHome = entity.TeamHome,
            TeamAway = entity.TeamAway,
            GoalsHome = entity.GoalsHome,
            GoalsAway = entity.GoalsAway,
            StartTime = entity.StartTime,
            StartMatchTime = entity.StartMatchTime,
            Status = (StreamStatus)Enum.Parse(typeof(StreamStatus), entity.Status)
        };
    }
}