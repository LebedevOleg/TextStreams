using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Enums;
using TextStreams.AppService.Contracts.Models;

namespace TextStreams.AppServices.Interfaces;

/// <summary>
/// Репозиторий стримов. 
/// </summary>
public interface IStreamRepository
{
    /// <summary>
    /// Создание стрима.
    /// </summary>
    /// <param name="request"> Запрос на создание стрима.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    public Task CreateStream(StreamServiceDto request, CancellationToken cancellationToken);

    /// <summary>
    /// Получение стримов.
    /// </summary>
    /// <param name="date"></param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    /// <returns> Список стримов.</returns>
    public Task<IEnumerable<StreamServiceDto>> GetStreams(DateOnly date, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление статуса стрима.
    /// </summary>
    /// <param name="request"> Статус стрима.</param>
    /// <param name="groupId"> Идентификатор стрима.</param>
    /// <param name="startMatchTime"> Время начала игры.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    public Task UpdateStreamStatus(
        StreamStatus request,
        long groupId,
        DateTime startMatchTime,
        CancellationToken cancellationToken);

    /// <summary>
    /// Обновление счета. 
    /// </summary>
    /// <param name="goalHome"> Домашний счет.</param>
    /// <param name="goalAway"> Гостевой счет.</param>
    /// <param name="groupId"> Идентификатор стрима.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    public Task UpdateGoals(int goalHome, int goalAway, long groupId, CancellationToken cancellationToken);
}