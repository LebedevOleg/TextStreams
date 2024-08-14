namespace TextStreams.Api.Contracts.Enums;

/// <summary>
/// Статус стрима.
/// </summary>
public enum StreamStatus
{
    /// <summary>
    /// Анонс.
    /// </summary>
    Announce = 0,
    
    /// <summary>
    /// В прямом эфире.
    /// </summary>
    Live = 1,
    
    /// <summary>
    /// Первая половина.
    /// </summary>
    FirstHalf,
    
    /// <summary>
    /// Вторая половина.
    /// </summary>
    SecondHalf,
    
    /// <summary>
    /// Перерыв во время матча.
    /// </summary>
    HalfTimeBreak,
    
    /// <summary>
    /// Первый овертайм.
    /// </summary>
    FirstOvertime,
    
    /// <summary>
    /// Второй овертайм.
    /// </summary>
    SecondOvertime,
    
    /// <summary>
    /// Завершен.
    /// </summary>
    End
}