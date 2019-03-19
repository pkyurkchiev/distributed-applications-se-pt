using System;
using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public class Movie : BaseEntity
    {
        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseCountry { get; set; }

        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
