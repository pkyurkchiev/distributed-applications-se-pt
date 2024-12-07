using System.ComponentModel.DataAnnotations;

namespace GustoHub.Data.Models
{
    public class ApiKey
    {
        public int Id { get; set; }

        [Required]
        public string Key { get; set; } = null!;

        [Required]
        public string Owner { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime? ExpirationDate { get; set; }

        public bool IsActive { get; set; } 
    }
}
