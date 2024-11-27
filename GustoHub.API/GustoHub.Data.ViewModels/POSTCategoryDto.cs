namespace GustoHub.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class POSTCategoryDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Category name cannot be bigger then 50 symbols.")]
        public string Name { get; set; } = string.Empty!;
    }
}
