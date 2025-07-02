using Microsoft.AspNetCore.Mvc;

namespace GameCatalogue.Api.Models
{
    public class UpdateGameFormModel
    {
        [FromRoute(Name = "id")]
        public long Id { get; set; }

        [FromForm]
        public string Name { get; set; } = default!;

        [FromForm]
        public decimal Price { get; set; }

        [FromForm]
        public DateTime? LastModified { get; set; }

        [FromForm]
        public string Platforms { get; set; } = default!;

        [FromForm]
        public IFormFile? ImageFile { get; set; }
    }
}
