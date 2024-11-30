using System.ComponentModel.DataAnnotations;

namespace GustoHub.Data.ViewModels.GET
{
    public class GETDishDto
    {
        public string Name { get; set; } = string.Empty!;

        public string Price { get; set; } = string.Empty;

        public int CategoryId { get; set; }
    }
}
