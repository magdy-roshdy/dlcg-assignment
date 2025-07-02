using GameCatalogue.Domain.Entities;

namespace GameCatalogue.Domain.Seeding
{
    public static class GameSeedData
    {
        public static List<Game> InitialGames { get; } = new List<Game>
        {
            new Game {
                Name        = "Mortal Kombat",
                Price       = 0m,
                AddedOn     = DateTime.UtcNow,
                ImagePath   = "/images/mortal-kombat.jpg",
                Platforms   = "pc,ps4,ps5"
            },
            new Game {
                Name        = "Mine Craft",
                Price       = 8.99m,
                AddedOn     = DateTime.UtcNow,
                ImagePath   = "/images/mine-craft.jpg",
                Platforms   = "pc,xbox1,ps4,ps5"
            },
            new Game {
                Name        = "EA SPORTS FC",
                Price       = 15.99m,
                AddedOn     = DateTime.UtcNow,
                ImagePath   = "/images/ea-sports.jpg",
                Platforms   = "pc,xbox1,xboxs"
            },
            new Game {
                Name        = "Rooftops & Alleys: The Parkour Game",
                Price       = 35.99m,
                AddedOn     = DateTime.UtcNow,
                ImagePath   = "/images/rooftops.jpg",
                Platforms   = "pc,ps4,ps5"
            },
            new Game {
                Name        = "STAR WARS™ Battlefront™ II: Celebration Edition",
                Price       = 65.99m,
                AddedOn     = DateTime.UtcNow,
                ImagePath   = "/images/start-wars.jpg",
                Platforms   = "pc,ps4,ps5"
            },
            new Game {
                Name        = "Forza Horizon 5 Standard Edition",
                Price       = 80.99m,
                AddedOn     = DateTime.UtcNow,
                ImagePath   = "/images/forza-horizon.jpg",
                Platforms   = "xbox1,xboxs"
            },
            new Game {
                Name        = "Resident Evil Village Gold Edition",
                Price       = 45.99m,
                AddedOn     = DateTime.UtcNow,
                ImagePath   = "/images/resident-evil-village.jpg",
                Platforms   = "pc"
            },
            new Game {
                Name        = "Insurgency: Sandstorm",
                Price       = 55.99m,
                AddedOn     = DateTime.UtcNow,
                ImagePath   = "/images/insurgency.jpg",
                Platforms   = "pc,xbox1,xboxs"
            },
            new Game {
                Name        = "LEGO® Harry Potter™ Collection",
                Price       = 21.99m,
                AddedOn     = DateTime.UtcNow,
                ImagePath   = "/images/lego-harry-potter.jpg",
                Platforms   = "pc,xbox1,xboxs"
            },
            new Game {
                Name        = "Batman: Return to Arkham",
                Price       = 34.99m,
                AddedOn     = DateTime.UtcNow,
                ImagePath   = "/images/batman-return-to-arkham.jpg",
                Platforms   = "pc,xbox1,ps5"
            }
        };
    }
}
