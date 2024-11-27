namespace GustoHub.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class POSTOrderDishDto
    {
        [Required(ErrorMessage = "Id of order is required.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Id of dish is required.")]
        public int DishId { get; set; }
    }
}
