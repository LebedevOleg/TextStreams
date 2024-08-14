using TextStreams.Api.Contracts.Dto;

namespace TextStreams.AppService.Contracts.Models;

public static class StreamMappingExtensions
{
    /// <summary>
    /// Преобразование запроса в DTO.
    /// </summary>
    /// <param name="request"> Запрос на создание стрима.</param>
    /// <returns> DTO с данными стрима.</returns>
    public static StreamServiceDto ToDto(this StreamRequest request)
    {
        return new StreamServiceDto
        {
            Name = string.Empty,
            TeamHome = request.TeamHome,
            TeamAway = request.TeamAway,
            GoalsHome = 0,
            GoalsAway = 0,
            StartTime = request.StartTime,
            StartMatchTime = request.StartTime,
        };
    }

    /// <summary>
    /// Преобразование DTO в ответ.
    /// </summary>
    /// <param name="stream"> DTO с данными стрима.</param>
    /// <returns> Возвращаемые данные стрима.</returns>
    public static StreamResponse ToResponse(this StreamServiceDto stream)
    {
        return new StreamResponse
        {
            Id = stream.Id,
            Name = stream.Name,
            TeamHome = stream.TeamHome,
            TeamAway = stream.TeamAway,
            GoalsHome = stream.GoalsHome,
            GoalsAway = stream.GoalsAway,
            StartTime = stream.StartTime,
            StartMatchTime = stream.StartMatchTime,
            Status = stream.Status
        };
    }
}