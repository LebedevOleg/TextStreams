using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Enums;

namespace TextStreams.Api.Contracts.Controllers;

/// <summary>
/// Интерфейс контроллера комментатора. 
/// </summary>
public interface ICommentatorController
{
    /// <summary>
    /// Создание стрима.
    /// </summary>
    /// <param name="request"> Данные стрима.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    public Task CreateStream(StreamRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление статуса стрима.
    /// </summary>
    /// <param name="request"> Статус стрима.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    public Task UpdateStreamStatus(UpdateStreamStatusDto request, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление счета.
    /// </summary>
    /// <param name="request"> Счет.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    public Task UpdateGoals(GoalRequest request, CancellationToken cancellationToken);
}