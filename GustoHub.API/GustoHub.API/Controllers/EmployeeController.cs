namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet("all-active")]
        public async Task<IActionResult> GetAllActiveEmployees()
        {
            var allEmployees = await employeeService.AllActiveAsync();
            return Ok(allEmployees);
        }
        [HttpGet("all-deactivated")]
        public async Task<IActionResult> GetAllDeActivatedEmployees()
        {
            var allEmployees = await employeeService.AllDeactivatedAsync();
            return Ok(allEmployees);
        }
        [HttpGet("{employeeName}")]
        public async Task<IActionResult> GetEmployeeByName(string employeeName)
        {
            var employee = await employeeService.GetByNameAsync(employeeName);

            if (employee == null)
            {
                return NotFound("Employee not found!");
            }

            return Ok(employeeName);
        }
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] POSTEmployeeDto employeeDto)
        {
            string responseMessage = await employeeService.AddAsync(employeeDto);
            return Ok(responseMessage);
        }
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateEmployee(Guid id)
        {
            if (!await employeeService.ExistsByIdAsync(id))
            {
                return NotFound("Employee not found!");
            }

            return Ok(await employeeService.ActivateAsync(id));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(PUTEmployeeDto employee, string id)
        {
            if (!await employeeService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound("Employee not found!");
            }

            return Ok(await employeeService.UpdateAsync(employee, id));
        }
        [HttpDelete("deactivate/{id}")]
        public async Task<IActionResult> DeactivateEmployee(Guid id)
        {
            if (!await employeeService.ExistsByIdAsync(id))
            {
                return NotFound("Employee not found!");
            }

            return Ok(await employeeService.DeactivateAsync(id));
        }
    }
}
