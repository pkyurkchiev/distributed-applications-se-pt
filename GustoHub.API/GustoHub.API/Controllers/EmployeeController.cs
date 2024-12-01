namespace GustoHub.API.Controllers
{
    using GustoHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Services.Services;

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
        public async Task<IActionResult> PostEmployee([FromBody] POSTEmployeeDto employeeDto)
        {
            string responseMessage = await employeeService.AddAsync(employeeDto);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(PUTEmployeeDto employee, string id)
        {
            return Ok(await employeeService.UpdateAsync(employee, id));
        }
    }
}
