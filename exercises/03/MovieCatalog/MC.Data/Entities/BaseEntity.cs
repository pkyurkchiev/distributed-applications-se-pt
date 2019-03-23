using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsActive { get; set; }
    }
}
