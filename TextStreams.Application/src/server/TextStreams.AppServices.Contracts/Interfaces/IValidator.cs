namespace TextStreams.AppService.Contracts.Interfaces;

public interface IValidator<in T>
{
    public void Validate(T obj);
}