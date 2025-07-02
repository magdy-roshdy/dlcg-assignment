namespace GameCatalogue.Application.Dtos
{
    public record GameDto(long Id,
        string Name,
        decimal Price,
        DateTime AddedOn,
        string ImagePath,
        DateTime? LastModified,
        string Platforms
    );
}
