using Microsoft.AspNetCore.Mvc;
using TextStreams.Api.Contracts.Controllers;
using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Enums;
using TextStreams.AppService.Contracts.Interfaces.Commentators;
using TextStreams.AppService.Contracts.Models;

namespace TextStreams.Host.Controllers;

/// <summary>
/// Контроллер комментатора.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CommentatorController : ICommentatorController
{
    private readonly ICreateStreamHandler _createStreamHandler;
    private readonly IUpdateStreamStatusHandler _updateStreamStatusHandler;
    private readonly IUpdateGoalsHandler _updateGoalsHandler;
    private readonly ILogger<CommentatorController> _logger;

    public CommentatorController(
        ICreateStreamHandler createStreamHandler,
        IUpdateStreamStatusHandler updateStreamStatusHandler,
        IUpdateGoalsHandler updateGoalsHandler,
        ILogger<CommentatorController> logger)
    {
        _createStreamHandler = createStreamHandler;
        _updateStreamStatusHandler = updateStreamStatusHandler;
        _updateGoalsHandler = updateGoalsHandler;
        _logger = logger;
    }

    [HttpPost("[action]")]
    public async Task CreateStream([FromBody] StreamRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"CreateStreamRequest: {request}");
        await _createStreamHandler.Handle(request, cancellationToken);
        _logger.LogInformation($"CreateStreamRequest: {request}; Completed;");
    }

    [HttpPut("[action]")]
    public async Task UpdateStreamStatus(
        [FromBody] UpdateStreamStatusDto request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"UpdateStreamStatusRequest: {request}");
        await _updateStreamStatusHandler.Handle(request, cancellationToken);
        _logger.LogInformation($"UpdateStreamStatusRequest: {request}; Completed;");
    }

    [HttpPut("[action]")]
    public async Task UpdateGoals(
        [FromBody] GoalRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"UpdateGoalsRequest: {request}");
        await _updateGoalsHandler.Handle(request, cancellationToken);
        _logger.LogInformation($"UpdateGoalsRequest: {request}; Completed;");
    }
}