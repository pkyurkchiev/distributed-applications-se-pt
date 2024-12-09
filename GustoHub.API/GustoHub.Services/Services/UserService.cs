namespace GustoHub.Services.Services
{
    using System;
    using BCrypt.Net;
    using GustoHub.Data.Models;
    using GustoHub.Data.Common;
    using System.Threading.Tasks;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly IRepository repository;
        private readonly IEmailService emailService;

        public UserService(
            IRepository repository,
            IEmailService emailService)
        {
            this.repository = repository;
            this.emailService = emailService;
        }

        public async Task<string> AddAsync(POSTUserDto userDto)
        {
            User user = new User()
            {
                Username = userDto.Username,
                Role = userDto.Role,
                PasswordHash = BCrypt.HashPassword(userDto.Password),
                CreatedAt = DateTime.Now,
                IsVerified = false
            };

            await repository.AddAsync(user);
            await repository.SaveChangesAsync();

            GETUserDto getUserDto = new GETUserDto()
            {
                Id = user.Id.ToString(),
                Username = user.Username,
                CreatedAt = user.CreatedAt.ToShortDateString(),
                Role = user.Role,
            };

            await emailService.SendAdminApprovalRequestAsync(getUserDto);

            return "User added Successfully!";
        }

        public async Task<bool> ExistsByIdAsync(Guid userId)
        {
            return await repository.AllAsReadOnly<User>().AnyAsync(u => u.Id == userId);
        }

        public async Task<GETUserDto> GetByIdAsync(Guid userId)
        {
            User? user = await repository.GetByIdAsync<User>(userId);

            GETUserDto userDto = new GETUserDto() 
            {
                Id = user.Id.ToString(),
                Username = user.Username,
                Role = user.Role,
                CreatedAt = user.CreatedAt.ToShortDateString(),
            };

            return userDto;
        }

        public async Task<string> UpdateAsync(PUTUserDto userDto, Guid userId)
        {
            User? user = await repository.GetByIdAsync<User>(userId);

            user.Role = userDto.Role;

            await repository.SaveChangesAsync();
            return "User updated Successfully!";
        }
    }
}
