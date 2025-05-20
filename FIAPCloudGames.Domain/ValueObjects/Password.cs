namespace FIAPCloudGames.Domain.ValueObjects
{
    public class Password
    {
        public string Hash { get; private set; }

        public Password() { } //EF exige para dar bind nesse value object

        public Password(string password)
        {
            if (!IsValid(password))
                throw new ArgumentException("Password must be at least 8 characters, include letters, numbers, and symbols.");

            Hash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool IsValid(string password) =>
            password.Length >= 8 &&
            password.Any(char.IsLetter) &&
            password.Any(char.IsDigit) &&
            password.Any(c => !char.IsLetterOrDigit(c));
    }
}
