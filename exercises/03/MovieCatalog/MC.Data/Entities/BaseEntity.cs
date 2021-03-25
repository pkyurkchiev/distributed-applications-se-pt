using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
