namespace GameCatalogue.Domain.Entities
{
    public class Game
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTime AddedOn { get; set; }
        public string ImagePath { get; set; } = default!;
        public DateTime? LastModified { get; set; }
        public string Platforms { get; set; } = default!;
    }
}
