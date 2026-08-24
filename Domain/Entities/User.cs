// Domain/Entities/User.cs
namespace Domain.Entities
{
    public enum Role
    {
        Member,
        Admin
    }

    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Member;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}