using Microsoft.EntityFrameworkCore;
using TextStreams.DataAccess.Models;

namespace TextStreams.DataAccess.Entity;

/// <summary>
/// Контекст БД.
/// </summary>
public class PostgresContext : DbContext
{
    /// <summary>
    /// Список стримов.
    /// </summary>
    public DbSet<StreamEntity> Streams { get; set; }

    /// <summary>
    /// Список сообщений в стриме.
    /// </summary>
    public DbSet<StreamMessageEntity> StreamMessages { get; set; }
    
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder.HasDefaultSchema("text_streams"));
    }
}