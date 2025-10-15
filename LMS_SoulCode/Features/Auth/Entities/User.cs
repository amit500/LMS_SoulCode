namespace LMS_SoulCode.Features.Auth.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Mobile { get; set; } = null!;        
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        // Forgot Password ke liye
        public string ResetToken { get; set; } = null!; // Token store
        public DateTime? ResetTokenExpiry { get; set; } // Expiry time (nullable)

        // Refresh Token ke liye
        public string RefreshToken { get; set; } = null!; // Refresh token store
        public DateTime? RefreshTokenExpiry { get; set; } // Expiry time (nullable)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
