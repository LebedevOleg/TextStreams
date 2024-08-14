namespace Commentator.Application;

/// <summary>
/// События матча.
/// </summary>
public enum MatchEvents
{
    /// <summary>
    /// Гол.
    /// </summary>
    Goal = 1,
    
    /// <summary>
    /// Желтая карточка.
    /// </summary>
    YellowCard,
    
    /// <summary>
    /// Красная карточка.
    /// </summary>
    RedCard,
    
    /// <summary>
    /// Половина игры.
    /// </summary>
    HalfTime,
    
    /// <summary>
    /// Овертайм.
    /// </summary>
    OverTime,
    
    /// <summary>
    /// Завершена.
    /// </summary>
    End,
    
    /// <summary>
    /// Замена.
    /// </summary>
    Swap,
    
    /// <summary>
    /// Проверка Видео ассистентом.
    /// </summary>
    VAR,
    
    /// <summary>
    /// Фол.
    /// </summary>
    Foul
}