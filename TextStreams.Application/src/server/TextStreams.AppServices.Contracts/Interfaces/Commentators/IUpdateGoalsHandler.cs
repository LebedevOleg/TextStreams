using TextStreams.Api.Contracts.Dto;

namespace TextStreams.AppService.Contracts.Interfaces.Commentators;

/// <summary>
/// Интерфейс обновления счетов.
/// </summary>
public interface IUpdateGoalsHandler : IHandler<GoalRequest>
{
}