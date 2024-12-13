using System.ComponentModel.DataAnnotations;

namespace GustoHub.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public bool IsVerified { get; set; }

        public Guid? EmployeeId { get; set; } 
        public virtual Employee? Employee { get; set; }

        public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();
    }
}
