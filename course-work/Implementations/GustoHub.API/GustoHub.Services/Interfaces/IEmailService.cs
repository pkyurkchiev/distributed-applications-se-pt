namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.ViewModels.GET;

    public interface IEmailService
    {
        Task SendAdminApprovalRequestAsync(GETUserDto userDto);
    }
}
