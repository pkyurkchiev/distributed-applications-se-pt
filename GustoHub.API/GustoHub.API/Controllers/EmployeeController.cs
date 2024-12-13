namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Infrastructure.Attributes;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpGet("all-active")]
        public async Task<IActionResult> GetAllActiveEmployees()
        {
            var allEmployees = await employeeService.AllActiveAsync();
            return Ok(allEmployees);
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpGet("all-deactivated")]
        public async Task<IActionResult> GetAllDeActivatedEmployees()
        {
            var allEmployees = await employeeService.AllDeactivatedAsync();
            return Ok(allEmployees);
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpGet("{employeeName}")]
        public async Task<IActionResult> GetEmployeeByName(string employeeName)
        {
            var employee = await employeeService.GetByNameAsync(employeeName);

            if (employee == null)
            {
                return NotFound(new {message = "Employee not found!" });
            }

            return Ok(employee);
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] POSTEmployeeDto employeeDto)
        {
            string responseMessage = await employeeService.AddAsync(employeeDto);
            return Ok(new { message = responseMessage });
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateEmployee(Guid id)
        {
            if (!await employeeService.ExistsByIdAsync(id))
            {
                return NotFound("Employee not found!");
            }

            string responseMessage = await employeeService.ActivateAsync(id);

            return Ok(new {message = responseMessage});
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(PUTEmployeeDto employee, string id)
        {
            if (!await employeeService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound("Employee not found!");
            }

            string responseMessage = await employeeService.UpdateAsync(employee, id);

            return Ok(new {message = responseMessage});
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpDelete("deactivate/{id}")]
        public async Task<IActionResult> DeactivateEmployee(Guid id)
        {
            if (!await employeeService.ExistsByIdAsync(id))
            {
                return NotFound("Employee not found!");
            }

            string responseMessage = await employeeService.DeactivateAsync(id);

            return Ok(new { message = responseMessage });
        }
    }
}
