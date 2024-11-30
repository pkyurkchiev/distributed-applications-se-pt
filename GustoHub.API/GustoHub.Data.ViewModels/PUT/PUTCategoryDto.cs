namespace GustoHub.Data.ViewModels.PUT
{
    using System.ComponentModel.DataAnnotations;

    public class PUTCategoryDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Category name cannot be bigger then 50 symbols.")]
        public string Name { get; set; } = string.Empty!;
    }
}
