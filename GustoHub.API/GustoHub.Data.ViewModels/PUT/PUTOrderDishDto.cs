namespace GustoHub.Data.ViewModels.PUT
{
    using System.ComponentModel.DataAnnotations;

    public class PUTOrderDishDto
    {
        [Required(ErrorMessage = "Id of order is required.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Id of dish is required.")]
        public int DishId { get; set; }
    }
}
