using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TextStreams.Api.Contracts.Controllers;
using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Enums;
using TextStreams.Api.Contracts.Hubs;
using TextStreams.AppService.Contracts.Interfaces.Clients;
using TextStreams.AppService.Contracts.Interfaces.Commentators;
using TextStreams.Host.Hubs;

namespace TextStreams.Host.Controllers;

/// <summary>
/// Контроллер клиента.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ClientController : IClientController
{
    private readonly IGetStreamsHandler _getStreamsHandler;
    private readonly ILogger<ClientController> _logger;

    public ClientController(IGetStreamsHandler getStreamsHandler, ILogger<ClientController> logger)
    {
        _getStreamsHandler = getStreamsHandler;
        _logger = logger;
    }

    [HttpPost("[action]")]
    public async Task<IEnumerator<StreamResponse>> GetStreams(
        [FromBody] DateOnly date,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"GetStreamsRequest: {date}");
        var result = await _getStreamsHandler.Handle(date, cancellationToken);
        _logger.LogInformation($"GetStreamsRequest: {date}; Completed;");
        return result;
    }
}