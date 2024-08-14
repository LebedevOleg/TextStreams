namespace TextStreams.AppService.Contracts.Models;

/// <summary>
/// DTO с сообщением.
/// </summary>
public class MessageServiceDto
{
    /// <summary>
    /// Текст сообщения.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Идентификатор стрима.
    /// </summary>
    public long StreamId { get; set; }
}