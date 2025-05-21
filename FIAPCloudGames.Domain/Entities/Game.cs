using FIAPCloudGames.Domain.Enumerators;
using FIAPCloudGames.Domain.Interfaces;
using FIAPCloudGames.Domain.ValueObjects;

namespace FIAPCloudGames.Domain.Entities
{
    public class Game : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Price Price { get; private set; }
        public GameCategory Category { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public Game()
        {
        }

        public Game(string name, string? description, Price price, GameCategory category, DateTime releaseDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            ReleaseDate = releaseDate;
            LastUpdate = DateTime.UtcNow;
        }
    }
}
