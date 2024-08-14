using TextStreams.Api.Contracts.Enums;

namespace TextStreams.AppService.Contracts.Models;

/// <summary>
/// DTO с данными стрима.
/// </summary>
public class StreamServiceDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Название стрима. 
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Название домашней команды.
    /// </summary>
    public string TeamHome { get; init; }

    /// <summary>
    /// Название гостевой команды.
    /// </summary>
    public string TeamAway { get; init; }

    /// <summary>
    /// Счет домашей команды.
    /// </summary>
    public int GoalsHome { get; init; }

    /// <summary>
    /// Счет гостей команды.
    /// </summary>
    public int GoalsAway { get; init; }

    /// <summary>
    /// Время начала стрима.
    /// </summary>
    public DateTime StartTime { get; init; }

    /// <summary>
    /// Статус стрима.
    /// </summary>
    public StreamStatus Status { get; init; }

    /// <summary>
    /// Время начала матча.
    /// </summary>
    public DateTime StartMatchTime { get; init; }
}