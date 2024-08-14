using TextStreams.AppService.Contracts.Models;

namespace TextStreams.AppServices.Interfaces;

/// <summary>
/// Репозиторий сообщений.
/// </summary>
public interface IMessagesRepository
{
    /// <summary>
    /// Получить сообщения стрима.
    /// </summary>
    /// <param name="streamId"> Идентификатор стрима.</param>
    /// <returns> Список сообщений.</returns>
    public Task<IEnumerator<string>> GetMessages(long streamId);

    /// <summary>
    /// Добавить сообщение.
    /// </summary>
    /// <param name="request"> Запрос для добавления сообщения.</param>
    public Task AddMessage(MessageServiceDto request);
}