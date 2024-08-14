namespace TextStreams.AppService.Contracts.Exceptions;

public class StreamValidateException : Exception
{
    public StreamValidateException(string message) : base(message)
    {
    }
}