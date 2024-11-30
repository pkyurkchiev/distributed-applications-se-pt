using System.ComponentModel.DataAnnotations;

namespace GustoHub.Data.ViewModels.GET
{
    public class GETEmployeeDto
    {
        public string Id { get; set; } = null!;
        
        public string Name { get; set; } = string.Empty!;

        public string Title { get; set; } = string.Empty!;

        public string HireDate { get; set; } = string.Empty!;

        public bool IsActive { get; set; }
    }
}
