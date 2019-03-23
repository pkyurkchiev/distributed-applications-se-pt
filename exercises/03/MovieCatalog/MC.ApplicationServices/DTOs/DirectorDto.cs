using MC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.ApplicationServices.DTOs
{
    public class DirectorDto : BaseDto
    {
        public DirectorDto() { }
        public DirectorDto(Director director)
            : base(director.Id, director.IsActive)
        {
            FirstName = director.FirstName;
            LastName = director.LastName;
            UserName = director.UserName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
