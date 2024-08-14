using TextStreams.Api.Contracts.Dto;

namespace TextStreams.AppService.Contracts.Models;

public static class MessageMappingExtensions
{
    /// <summary>
    /// Преобразование сущности в DTO
    /// </summary>
    /// <param name="entity"> Сущность сообщения.</param>
    /// <returns> DTO с сообщением.</returns>
    public static MessageServiceDto ToDto(this StreamMessageRequest entity)
    {
        return new MessageServiceDto
        {
            Text = entity.Text,
            StreamId = entity.StreamId
        };
    }
}