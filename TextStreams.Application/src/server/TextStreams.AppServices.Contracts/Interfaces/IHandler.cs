namespace TextStreams.AppService.Contracts.Interfaces;

public interface IHandler
{
    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены.</param>
    public Task Handle(CancellationToken cancellationToken);
}

public interface IHandler<in TRequest>
{
    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="request"> Запрос.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    public Task Handle(TRequest request, CancellationToken cancellationToken);
}

public interface IHandler<in TRequest, TResponse>
{
    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="request"> Запрос.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    /// <returns> Ответ.</returns>
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}