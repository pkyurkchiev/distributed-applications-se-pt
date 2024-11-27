namespace GustoHub.API.Controllers
{
    using GustoHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            string responseMessage = await employeeService.AddAsync(employee);
            return Ok(responseMessage);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var allEmployees = await employeeService.AllAsync();
            return Ok(allEmployees);
        }
        [HttpGet("{employeeName}")]
        public async Task<IActionResult> GetEmployeeByName(string employeeName)
        {
            var employee = await employeeService.GetByNameAsync(employeeName);
            return Ok(employeeName);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEmployee(Guid id)
        {
            return Ok(await employeeService.Remove(id));
        }
    }
}
