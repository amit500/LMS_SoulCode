namespace LMS_SoulCode.Features.Auth.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserRole { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
