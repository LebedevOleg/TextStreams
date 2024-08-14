using TextStreams.Api.Contracts.Dto;

namespace TextStreams.Api.Contracts.Controllers;

/// <summary>
/// Интерфейс контроллера клиента.
/// </summary>
public interface IClientController
{
    /// <summary>
    /// Получение стримов.
    /// </summary>
    /// <param name="date"> Дата.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    /// <returns> Список стримов.</returns>
    public Task<IEnumerator<StreamResponse>> GetStreams(DateOnly date, CancellationToken cancellationToken);
}