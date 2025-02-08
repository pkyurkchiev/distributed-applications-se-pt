namespace GustoHub.Data.ViewModels.GET
{
    public class GETUserDto
    {
        public string Id { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string CreatedAt { get; set; } = null!;

        public bool IsVerified { get; set; }
    }
}
