using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextStreams.DataAccess.Models;

/// <summary>
/// Сущность сообщения стрима.
/// </summary>
[Table("stream_message")]
public class StreamMessageEntity
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор стрима.
    /// </summary>
    [Column("stream_id")]
    [ForeignKey("StreamId")]
    public long StreamId { get; set; }

    /// <summary>
    /// Ссылка на стрим.
    /// </summary>
    public StreamEntity Stream { get; set; }

    /// <summary>
    /// Сообщение.
    /// </summary>
    [Column("message")]
    public string Message { get; set; }
}