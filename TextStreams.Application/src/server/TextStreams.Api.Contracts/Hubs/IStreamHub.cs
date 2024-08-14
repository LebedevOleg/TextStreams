namespace TextStreams.Api.Contracts.Hubs;

/// <summary>
/// Хаб стримов. 
/// </summary>
public interface IStreamHub
{
    /// <summary>
    /// Вход в группу.
    /// </summary>
    /// <param name="groupName"> Имя группы.</param>
    /// <param name="streamId"> Идентификатор стрима.</param>
    public Task Enter(string groupName, long streamId);

    /// <summary>
    /// Выход из группы.
    /// </summary>
    /// <param name="groupName"> Имя группы.</param>
    public Task Leave(string groupName);

    /// <summary>
    /// Отправка сообщения.
    /// </summary>
    /// <param name="message"> Сообщение.</param>
    public Task SendMessage(string message);
}