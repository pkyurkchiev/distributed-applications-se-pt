namespace GustoHub.API.Controllers
{
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Infrastructure.Attributes;
    using GustoHub.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var userDto = await userService.GetByIdAsync(Guid.Parse(userId));
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] POSTUserDto userDto)
        {
            if (await userService.ExistsByUsernameAsync(userDto.Username))
            {
                return BadRequest("There is an existing user with that username. Please choose another.");
            }
            string responseMessage = await userService.AddAsync(userDto);

            return Ok(responseMessage);
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(PUTUserDto user, string id)
        {
            if (!await userService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound("User not found!");
            }


            return Ok(await userService.UpdateAsync(user, Guid.Parse(id)));
        }
    }
}
