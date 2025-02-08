using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GustoHub.Data.Models
{
    public class ApiKey
    {
        public int Id { get; set; }

        [Required]
        public string Key { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime? ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;
    }
}
