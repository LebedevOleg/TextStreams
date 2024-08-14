namespace TextStreams.Api.Contracts.Dto;

/// <summary>
/// Запрос на создание стрима.
/// </summary>
public record StreamRequest
{
    /// <summary>
    /// Название домашней команды.
    /// </summary>
    public string TeamHome { get; init; }

    /// <summary>
    /// Название гостей команды.
    /// </summary>
    public string TeamAway { get; init; }
    
    /// <summary>
    /// Время начала стрима.
    /// </summary>
    public DateTime StartTime { get; init; }
}