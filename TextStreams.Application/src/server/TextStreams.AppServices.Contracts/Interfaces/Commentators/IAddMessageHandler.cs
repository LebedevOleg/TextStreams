using TextStreams.Api.Contracts.Dto;

namespace TextStreams.AppService.Contracts.Interfaces.Commentators;

/// <summary>
/// Интерфейс добавления сообщения.
/// </summary>
public interface IAddMessageHandler
{
    /// <summary>
    /// Добавление сообщения.
    /// </summary>
    /// <param name="message"> Сообщение.</param>
    public Task Handle(StreamMessageRequest message);
}