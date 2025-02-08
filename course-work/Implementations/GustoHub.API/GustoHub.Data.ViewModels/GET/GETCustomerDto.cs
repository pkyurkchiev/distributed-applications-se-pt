using System.ComponentModel.DataAnnotations;

namespace GustoHub.Data.ViewModels.GET
{
    public class GETCustomerDto
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
