namespace TextStreams.AppService.Contracts.Interfaces.Clients;

/// <summary>
/// Интерфейс получения сообщений.
/// </summary>
public interface IGetMessagesHandler
{
    /// <summary>
    /// Интерфейс получения сообщений.
    /// </summary>
    /// <param name="streamId"> Идентификатор стрима.</param>
    /// <returns> Список сообщений.</returns>
    public Task<IEnumerator<string>> Handle(long streamId);
}