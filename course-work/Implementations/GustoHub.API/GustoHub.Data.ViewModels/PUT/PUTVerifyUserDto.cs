namespace GustoHub.Data.ViewModels.PUT
{
    using System.ComponentModel.DataAnnotations;

    public class PUTVerifyUserDto
    {
        [Required]
        public bool IsVerified { get; set; }
    }
}
