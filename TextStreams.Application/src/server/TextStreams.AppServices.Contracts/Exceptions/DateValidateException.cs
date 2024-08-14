namespace TextStreams.AppService.Contracts.Exceptions;

public class DateValidateException : Exception
{
    public DateValidateException(string message) : base(message)
    {
    }
}