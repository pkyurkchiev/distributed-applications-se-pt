using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GustoHub.Data.ViewModels.GET
{
    public class GETOrdersDto
    {
        public string Id { get; set; } = null!;
        public string OrderDate { get; set; } = null!;

        public string CompletionDate { get; set; } = null!;

        public string TotalAmount { get; set; } = null!;

        public string CustomerId { get; set; } = null!;

        public string EmployeeId { get; set; } = null!;

    }
}
