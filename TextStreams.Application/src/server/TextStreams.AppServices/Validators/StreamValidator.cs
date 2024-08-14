using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using TextStreams.Api.Contracts.Dto;
using TextStreams.AppService.Contracts.Exceptions;
using TextStreams.AppService.Contracts.Interfaces;

namespace TextStreams.AppServices.Validators;

public class StreamValidator : IValidator<StreamRequest>
{
    private readonly ILogger<StreamValidator> _logger;

    public StreamValidator(ILogger<StreamValidator> logger)
    {
        _logger = logger;
    }

    public void Validate(StreamRequest obj)
    {
        _logger.LogInformation($"Validate: {obj}");
        List<string> errors = new List<string>();
        if (string.IsNullOrWhiteSpace(obj.TeamHome) || string.IsNullOrWhiteSpace(obj.TeamAway))
        {
            errors.Add("TeamHome and TeamAway should not be empty");
        }

        if (obj.StartTime < DateTime.UtcNow)
        {
            errors.Add("StartTime should not be in the past");
        }

        if (errors.Count > 0)
        {
            throw new StreamValidateException(string.Join("; ", errors));
        }

        _logger.LogInformation($"Validate: {obj}; Completed");
    }
}