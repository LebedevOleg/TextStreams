using TextStreams.Api.Contracts.Dto;
using TextStreams.AppService.Contracts.Interfaces.Commentators;
using TextStreams.AppServices.Interfaces;

namespace TextStreams.AppServices.Handlers;

internal class UpdateGoalsHandler : IUpdateGoalsHandler
{
    private readonly IStreamRepository _streamRepository;

    public UpdateGoalsHandler(IStreamRepository streamRepository)
    {
        _streamRepository = streamRepository;
    }

    public Task Handle(GoalRequest request, CancellationToken cancellationToken)
    {
        return _streamRepository.UpdateGoals(request.GoalHome, request.GoalAway, request.GroupId, cancellationToken);
    }
}