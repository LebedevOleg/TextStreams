namespace TextStreams.Api.Contracts.Dto;

/// <summary>
/// Запрос на изменение счета.
/// </summary>
public record GoalRequest
{
    /// <summary>
    /// Идентификатор группы.
    /// </summary>
    public long GroupId { get; init; }
    
    /// <summary>
    /// Счет дома. 
    /// </summary>
    public int GoalHome { get; init; }
    
    /// <summary>
    /// Счет гостевой.
    /// </summary>
    public int GoalAway { get; init; }
}