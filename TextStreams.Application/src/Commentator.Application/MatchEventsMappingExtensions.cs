namespace Commentator.Application;

public static class MatchEventsMappingExtensions
{
    /// <summary>
    /// Маппинг событий матча в строку.
    /// </summary>
    /// <param name="matchEvent">Событие.</param>
    /// <returns> Строка.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Ошибка маппинга, состояния не существует.</exception>
    public static string Map(this MatchEvents matchEvent)
    {
        switch (matchEvent)
        {
            case MatchEvents.Goal:
                return "Гол";
            case MatchEvents.YellowCard:
                return "Желтая карточка";
            case MatchEvents.RedCard:
                return "Красная карточка";
            case MatchEvents.HalfTime:
                return "Половина игры";
            case MatchEvents.OverTime:
                return "Овертайм";
            case MatchEvents.End:
                return "Завершена";
            case MatchEvents.Swap:
                return "Замена";
            case MatchEvents.VAR:
                return "VAR";
            case MatchEvents.Foul:
                return "Фол";
            default:
                throw new ArgumentOutOfRangeException(nameof(matchEvent), matchEvent, null);
        }
    }
}