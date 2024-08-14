using TextStreams.Api.Contracts.Dto;
using TextStreams.AppService.Contracts.Interfaces;
using TextStreams.AppService.Contracts.Interfaces.Commentators;
using TextStreams.AppService.Contracts.Models;
using TextStreams.AppServices.Interfaces;

namespace TextStreams.AppServices.Handlers;

internal class CreateStreamHandler : ICreateStreamHandler
{
    private readonly IStreamRepository _streamRepository;
    private readonly IValidator<StreamRequest> _validator;

    public CreateStreamHandler(IStreamRepository streamRepository, IValidator<StreamRequest> validator)
    {
        _streamRepository = streamRepository;
        _validator = validator;
    }

    public async Task Handle(StreamRequest request, CancellationToken cancellationToken)
    {
        _validator.Validate(request);
        await _streamRepository.CreateStream(request.ToDto(), cancellationToken);
    }
}