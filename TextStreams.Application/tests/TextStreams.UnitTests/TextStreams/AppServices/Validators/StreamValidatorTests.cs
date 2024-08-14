using Microsoft.Extensions.Logging.Abstractions;
using TextStreams.Api.Contracts.Dto;
using TextStreams.AppService.Contracts.Exceptions;
using TextStreams.AppService.Contracts.Interfaces;
using TextStreams.AppServices.Validators;
using Xunit;

namespace TextStreams.UnitTests.TextStreams.AppServices.Validators;

public class StreamValidatorTests
{
    private readonly IValidator<StreamRequest> _validator;

    public StreamValidatorTests()
    {
        _validator = new StreamValidator(new NullLogger<StreamValidator>());
    }

    [Fact]
    public void Validate_InvalidStreamTeamHome_ThrowsStreamValidateException()
    {
        var obj = new StreamRequest
        {
            TeamHome = "",
            TeamAway = "Team",
            StartTime = DateTime.UtcNow.AddHours(1)
        };

        Assert.Throws<StreamValidateException>(() => _validator.Validate(obj));
    }

    [Fact]
    public void Validate_InvalidStreamTeamAway_ThrowsStreamValidateException()
    {
        var obj = new StreamRequest
        {
            TeamHome = "Team",
            TeamAway = "",
            StartTime = DateTime.UtcNow.AddHours(1)
        };

        Assert.Throws<StreamValidateException>(() => _validator.Validate(obj));
    }

    [Fact]
    public void Validate_InvalidStreamStartTime_ThrowsStreamValidateException()
    {
        var obj = new StreamRequest
        {
            TeamHome = "Team",
            TeamAway = "Team",
            StartTime = DateTime.UtcNow.AddHours(-1)
        };

        Assert.Throws<StreamValidateException>(() => _validator.Validate(obj));
    }

    [Fact]
    public void Validate_ValidStreamRequest_DoesNotThrow()
    {
        var obj = new StreamRequest
        {
            TeamHome = "Team",
            TeamAway = "Team",
            StartTime = DateTime.UtcNow.AddHours(1).ToUniversalTime()
        };

        _validator.Validate(obj);
    }
}