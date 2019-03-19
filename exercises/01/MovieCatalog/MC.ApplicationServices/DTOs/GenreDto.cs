using MC.Data.Entities;
using System;

namespace MC.ApplicationServices.DTOs
{
    public class GenreDto : BaseDto
    {
        public GenreDto() { }
        public GenreDto(Genre genre)
        {
            Id = genre.Id; 
            Name = genre.Name;
            IsActive = genre.IsActive;
        }

        public string Name { get; set; }
    }
}
