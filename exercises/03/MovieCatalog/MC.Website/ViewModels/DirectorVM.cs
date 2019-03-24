using System.ComponentModel.DataAnnotations;

namespace MC.Website.ViewModels
{
    public class DirectorVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}