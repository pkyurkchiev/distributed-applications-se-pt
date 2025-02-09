namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Infrastructure.Attributes;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        /// <summary>
        /// Retrieves all active employees. (Admin Only, API Key Required)
        /// </summary>
        /// <returns>A list of active employees.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpGet("all-active")]
        public async Task<IActionResult> GetAllActiveEmployees()
        {
            var allEmployees = await employeeService.AllActiveAsync();
            return Ok(allEmployees);
        }

        /// <summary>
        /// Retrieves all deactivated employees. (Admin Only, API Key Required)
        /// </summary>
        /// <returns>A list of deactivated employees.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpGet("all-deactivated")]
        public async Task<IActionResult> GetAllDeactivatedEmployees()
        {
            var allEmployees = await employeeService.AllDeactivatedAsync();
            return Ok(allEmployees);
        }

        /// <summary>
        /// Retrieves an employee by name. (Admin Only, API Key Required)
        /// </summary>
        /// <param name="employeeName">The name of the employee.</param>
        /// <returns>The employee if found, otherwise a 404 response.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpGet("{employeeName}")]
        public async Task<IActionResult> GetEmployeeByName(string employeeName)
        {
            var employee = await employeeService.GetByNameAsync(employeeName);

            if (employee == null)
            {
                return NotFound(new { message = "Employee not found!" });
            }

            return Ok(employee);
        }

        /// <summary>
        /// Adds a new employee. (Admin Only, API Key Required)
        /// </summary>
        /// <param name="employeeDto">The employee data to be added.</param>
        /// <param name="userId">The ID of the user creating the employee.</param>
        /// <returns>A success message.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] POSTEmployeeDto employeeDto, Guid userId)
        {
            string responseMessage = await employeeService.AddAsync(employeeDto, userId);
            return Ok(new { message = responseMessage });
        }

        /// <summary>
        /// Activates a deactivated employee by ID. (Admin Only, API Key Required)
        /// </summary>
        /// <param name="id">The ID of the employee to activate.</param>
        /// <returns>A success message or a 404 response if the employee is not found.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateEmployee(Guid id)
        {
            if (!await employeeService.ExistsByIdAsync(id))
            {
                return NotFound(new { message = "Employee not found!" });
            }

            string responseMessage = await employeeService.ActivateAsync(id);
            return Ok(new { message = responseMessage });
        }

        /// <summary>
        /// Updates an existing employee by their ID. (Admin Only, API Key Required)
        /// </summary>
        /// <param name="employee">The updated employee data.</param>
        /// <param name="id">The ID of the employee to update.</param>
        /// <returns>A success message or a 404 response if the employee is not found.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(PUTEmployeeDto employee, string id)
        {
            if (!await employeeService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound(new { message = "Employee not found!" });
            }

            string responseMessage = await employeeService.UpdateAsync(employee, id);
            return Ok(new { message = responseMessage });
        }

        /// <summary>
        /// Deactivates an employee by their ID. (Admin Only, API Key Required)
        /// </summary>
        /// <param name="id">The ID of the employee to deactivate.</param>
        /// <returns>A success message or a 404 response if the employee is not found.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpDelete("deactivate/{id}")]
        public async Task<IActionResult> DeactivateEmployee(Guid id)
        {
            if (!await employeeService.ExistsByIdAsync(id))
            {
                return NotFound(new { message = "Employee not found!" });
            }

            string responseMessage = await employeeService.DeactivateAsync(id);
            return Ok(new { message = responseMessage });
        }
    }
}
