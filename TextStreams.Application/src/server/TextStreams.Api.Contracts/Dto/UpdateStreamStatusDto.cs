using TextStreams.Api.Contracts.Enums;

namespace TextStreams.Api.Contracts.Dto;

/// <summary>
/// Запрос на изменение статуса стрима.
/// </summary>
public record UpdateStreamStatusDto
{
    /// <summary>
    /// Идентификатор стрима.
    /// </summary>
    public long StreamId { get; init; }

    /// <summary>
    /// Статус стрима.
    /// </summary>
    public StreamStatus Status { get; init; }

    /// <summary>
    /// Время начала игры.
    /// </summary>
    public DateTime StartMatchTime { get; init; }
}