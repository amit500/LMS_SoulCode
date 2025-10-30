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
        public string? ResetToken { get; set; }  
        public DateTime? ResetTokenExpiry { get; set; } 
        public string? RefreshToken { get; set; } 
        public DateTime? RefreshTokenExpiry { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
