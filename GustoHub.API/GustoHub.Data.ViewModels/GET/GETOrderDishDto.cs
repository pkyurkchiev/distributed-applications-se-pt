using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GustoHub.Data.ViewModels.GET
{
    public class GETOrderDishDto
    {
        public string OrderDate { get; set; } = null!;

        public string? OrderCompletionDate { get; set; }

        public string OrderTotalAmount { get; set; } = null!;

        public string OrderCustomerId { get; set; } = null!;

        public string OrderEmployeeId { get; set; } = null!;

        public string DishName { get; set; } = null!;

        public string DishPrice { get; set; } = null!;

        public int DishCategoryId { get; set; }
    }
}
