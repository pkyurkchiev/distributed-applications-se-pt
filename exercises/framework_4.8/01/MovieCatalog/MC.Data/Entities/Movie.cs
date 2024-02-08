using System;
using System.ComponentModel.DataAnnotations;

namespace MC.Data.Entities
{
    public class Movie : BaseEntity
    {
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Country { get; set; }
        public string ScreenWriter { get; set; }

        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
