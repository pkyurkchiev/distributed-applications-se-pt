using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GustoHub.Data.ViewModels.GET
{
    public class GETCategoryDto
    {
        public string Name { get; set; } = null!;

        [JsonPropertyName("dishes")]
        public List<GETDishDto> DishDtos { get; set; } = new List<GETDishDto>();

    }
}
