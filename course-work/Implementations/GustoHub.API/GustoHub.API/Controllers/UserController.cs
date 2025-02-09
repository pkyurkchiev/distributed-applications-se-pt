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

        /// <summary>
        /// Retrieves a user by ID. (Admin Only, API Key Required)
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A user details if found.</returns>
        [APIKeyRequired]
        [AuthorizeRole("Admin")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var userDto = await userService.GetByIdAsync(Guid.Parse(userId));
            return Ok(userDto);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The user data to be created.</param>
        /// <returns>A success message or error.</returns>
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] POSTUserDto userDto)
        {
            if (await userService.ExistsByUsernameAsync(userDto.Username))
            {
                return BadRequest("There is an existing user with that username. Please choose another.");
            }

            try
            {
                string responseMessage = await userService.AddAsync(userDto);

                return Ok(new { message = responseMessage + "An email is sent to an admin, to verify the user."});
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during user verification: {e.Message}");
                return StatusCode(500, new { error = "An error occurred while verifying the user." });
            }
        }

        /// <summary>
        /// Updates a user's details. (Admin Only, API Key Required)
        /// </summary>
        /// <param name="id">The unique identifier of the user to be updated.</param>
        /// <param name="user">The updated user data.</param>
        /// <returns>A success message or error.</returns>
        [APIKeyRequired]
        [AuthorizeRole("Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(PUTUserDto user, string id)
        {
            if (!await userService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound("User not found!");
            }

            string responseMessage = await userService.UpdateAsync(user, Guid.Parse(id));

            return Ok(new {message = responseMessage});
        }

        /// <summary>
        /// Verifies a user by ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be verified.</param>
        /// <returns>A success message or error.</returns>
        [HttpGet("verify")]
        public async Task<IActionResult> VerifyUser(Guid userId)
        {
            try
            {
                var user = await userService.GetByIdAsync(userId);
                if (user == null)
                {
                    return NotFound(new { error = "User not found." });
                }

                if (user.IsVerified)
                {
                    return BadRequest(new { message = "User is already verified." });
                }

                var updateDto = new PUTVerifyUserDto
                {
                    IsVerified = true,
                };

                await userService.VerifyAsync(updateDto, userId);

                return Ok(new { message = "User successfully verified." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during user verification: {ex.Message}");
                return StatusCode(500, new { error = "An error occurred while verifying the user." });
            }
        }
    }
}
