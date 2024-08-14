using TextStreams.Api.Contracts.Dto;
using TextStreams.AppService.Contracts.Exceptions;
using TextStreams.AppService.Contracts.Interfaces.Clients;
using TextStreams.AppService.Contracts.Interfaces.Commentators;
using TextStreams.AppService.Contracts.Models;
using TextStreams.AppServices.Interfaces;

namespace TextStreams.AppServices.Handlers;

internal class GetStreamsHandler : IGetStreamsHandler
{
    private readonly IStreamRepository _streamRepository;

    public GetStreamsHandler(IStreamRepository streamRepository)
    {
        _streamRepository = streamRepository;
    }

    public async Task<IEnumerator<StreamResponse>> Handle(DateOnly request, CancellationToken cancellationToken)
    {
        var result = await _streamRepository.GetStreams(request, cancellationToken);
        return result.Select(x => x.ToResponse()).GetEnumerator();
    }
}