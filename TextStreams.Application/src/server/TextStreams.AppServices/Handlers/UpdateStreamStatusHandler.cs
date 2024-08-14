using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Enums;
using TextStreams.AppService.Contracts.Interfaces.Commentators;
using TextStreams.AppServices.Interfaces;

namespace TextStreams.AppServices.Handlers;

internal class UpdateStreamStatusHandler : IUpdateStreamStatusHandler
{
    private readonly IStreamRepository _streamRepository;

    public UpdateStreamStatusHandler(IStreamRepository streamRepository)
    {
        _streamRepository = streamRepository;
    }

    public Task Handle(UpdateStreamStatusDto request, CancellationToken cancellationToken)
    {
        return _streamRepository.UpdateStreamStatus(request.Status, request.StreamId, request.StartMatchTime, cancellationToken);
    }
}