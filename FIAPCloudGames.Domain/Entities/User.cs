using FIAPCloudGames.Domain.Enumerators;
using FIAPCloudGames.Domain.Interfaces;
using FIAPCloudGames.Domain.ValueObjects;

namespace FIAPCloudGames.Domain.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public UserRole Role { get; private set; }

        private User() { }

        public User(string name, string email, string password, UserRole role = UserRole.Customer)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = new Email(email);
            Password = new Password(password);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Role = role;
        }
    }
}
