using TextStreams.AppService.Contracts.Models;

namespace TextStreams.DataAccess.Models;

public static class StreamMessageEntityExtensions
{
    /// <summary>
    /// Преобразование DTO в сущность.
    /// </summary>
    /// <param name="request"> DTO с сообщением.</param>
    /// <returns> Сущность сообщения.</returns>
    public static StreamMessageEntity ToEntity(this MessageServiceDto request)
    {
        return new StreamMessageEntity
        {
            StreamId = request.StreamId,
            Message = request.Text
        };
    }
}