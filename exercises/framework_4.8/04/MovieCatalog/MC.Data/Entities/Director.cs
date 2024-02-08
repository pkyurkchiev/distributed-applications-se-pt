using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public class Director : BaseEntity
    {
        [Required]
        [StringLength(300)]
        public string FirstName { get; set; }
        [StringLength(300)]
        public string LastName { get; set; }
        [Required]
        [StringLength(300)]
        public string UserName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
