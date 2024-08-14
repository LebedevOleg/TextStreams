using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextStreams.DataAccess.Models;

/// <summary>
/// Сущность стрима.
/// </summary>
[Table("stream")]
public class StreamEntity
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// Название стрима.
    /// </summary>
    [Column("name")] [MaxLength(255)] public string Name { get; set; }

    /// <summary>
    /// Название домашней команды.
    /// </summary>
    [Column("team_home")] [MaxLength(100)] public string TeamHome { get; set; }

    /// <summary>
    /// Название гостей команды. 
    /// </summary>
    [Column("team_away")] [MaxLength(100)] public string TeamAway { get; set; }

    /// <summary>
    /// Голы домашей команды.
    /// </summary>
    [Column("goals_home")] public int GoalsHome { get; set; }

    /// <summary>
    /// Голы гостей команды.
    /// </summary>
    [Column("goals_away")] public int GoalsAway { get; set; }

    /// <summary>
    /// Время начала стрима.
    /// </summary>
    [Column("start_time")]
    public DateTime StartTime
    {
        get => _startTime.ToLocalTime();
        set => _startTime = value.ToUniversalTime();
    }

    private DateTime _startTime;

    /// <summary>
    /// Время начала матча.
    /// </summary>
    [Column("start_match_time")]
    public DateTime StartMatchTime
    {
        get => _startMatchTime.ToLocalTime();
        set => _startMatchTime = value.ToUniversalTime();
    }

    private DateTime _startMatchTime;

    /// <summary>
    /// Статус стрима.
    /// </summary>
    [Column("status")] public string Status { get; set; }
}