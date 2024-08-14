namespace TextStreams.Api.Contracts.Dto;

/// <summary>
/// Запрос на добавление сообщения.
/// </summary>
public record StreamMessageRequest
{
    /// <summary>
    /// Текст сообщения.
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// Идентификатор стрима.
    /// </summary>
    public long StreamId { get; init; }

    /// <summary>
    /// Идентификатор группы.
    /// </summary>
    public string GroupName { get; init; }
}