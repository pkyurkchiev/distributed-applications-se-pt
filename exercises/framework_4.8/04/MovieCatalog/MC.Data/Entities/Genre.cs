using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string GenreName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
