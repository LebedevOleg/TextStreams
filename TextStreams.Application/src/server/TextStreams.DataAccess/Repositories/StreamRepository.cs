using Microsoft.EntityFrameworkCore;
using TextStreams.Api.Contracts.Enums;
using TextStreams.AppService.Contracts.Models;
using TextStreams.AppServices.Interfaces;
using TextStreams.DataAccess.Entity;
using TextStreams.DataAccess.Models;

namespace TextStreams.DataAccess.Repositories;

public class StreamRepository : IStreamRepository
{
    private readonly PostgresContext _context;

    public StreamRepository(PostgresContext context)
    {
        _context = context;
    }

    public async Task CreateStream(StreamServiceDto request, CancellationToken cancellationToken)
    {
        var entity = request.ToEntity();
        entity.StartTime = entity.StartTime.ToUniversalTime();
        entity.Name = $"{entity.TeamHome} : {entity.TeamAway} ({entity.StartTime:dd.MM.yyyy HH:mm})";

        await _context.Streams.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<IEnumerable<StreamServiceDto>> GetStreams(DateOnly date, CancellationToken cancellationToken)
    {
        var entities = _context.Streams
            .Where(x => date.Equals(DateOnly.FromDateTime(x.StartTime)))
            .Select(x => x.ToDto());
        return Task.FromResult(entities.AsEnumerable());
    }

    public async Task UpdateStreamStatus(StreamStatus request, long groupId, DateTime startMatchTime, CancellationToken cancellationToken)
    {
        var entity = await _context.Streams.FindAsync(groupId, cancellationToken);

        if (entity == null)
            return;

        entity.Status = request.ToString();
        entity.StartTime = entity.StartTime.ToUniversalTime();
        entity.StartMatchTime = startMatchTime.ToUniversalTime();

        _context.Streams.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateGoals(int goalHome, int goalAway, long groupId, CancellationToken cancellationToken)
    {
        var entity = await _context.Streams.FindAsync(groupId, cancellationToken);

        if (entity == null)
            return;

        entity.GoalsHome = goalHome;
        entity.GoalsAway = goalAway;
        entity.StartTime = entity.StartTime.ToUniversalTime();
        entity.StartMatchTime = entity.StartMatchTime.ToUniversalTime();

        _context.Streams.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}