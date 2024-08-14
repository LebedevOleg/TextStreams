using TextStreams.Api.Contracts.Enums;

namespace TextStreams.Api.Contracts.Dto;

/// <summary>
/// Возвращаемые данные стрима.
/// </summary>
public record StreamResponse
{
    /// <summary>
    /// Идентификатор стрима.
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
    /// Название гостей команды.
    /// </summary>
    public string TeamAway { get; init; }

    /// <summary>
    /// Количество голов в домашней команде.
    /// </summary>
    public int GoalsHome { get; init; }

    /// <summary>
    /// Количество голов в гостей команде.
    /// </summary>
    public int GoalsAway { get; init; }

    /// <summary>
    /// Статус стрима.
    /// </summary>
    public StreamStatus Status { get; init; }

    /// <summary>
    /// Время начала стрима.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Время начала матча.
    /// </summary>
    public DateTime StartMatchTime { get; set; }
}