namespace GustoHub.Data.ViewModels.POST
{
    using System.ComponentModel.DataAnnotations;

    public class POSTOrderDto
    {
        [DisplayFormat(DataFormatString = "dd/MM/yyyy HH:mm")]
        public string? CompletionDate { get; set; }

        public string CustomerId { get; set; } = null!;

        public string EmployeeId { get; set; } = null!;
    }
}
