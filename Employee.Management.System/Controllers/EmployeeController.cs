using Employee.Management.System.DTOS;
using Employee.Management.System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Management.System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
        public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService service)
        {
            _employeeService = service;
        }
        // GET: api/employee
        [HttpGet]
            public async Task<IActionResult> GetAll(
                [FromQuery] string? name,
                [FromQuery] int? departmentId,
                [FromQuery] string? status,
                [FromQuery] DateTime? hireDateFrom,
                [FromQuery] DateTime? hireDateTo,
                [FromQuery] string? sortBy,
                [FromQuery] bool isAscending = true)
            {
                var result = await _employeeService.GetAllAsync(name, departmentId, status, hireDateFrom, hireDateTo, sortBy, isAscending);
                return Ok(result);
            }

            // GET: api/employee/5
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var result = await _employeeService.GetByIdAsync(id);
                return result.IsSuccess ? Ok(result) : NotFound(result);
            }

            // POST: api/employee
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] EmployeeDto dto)
            {
                var result = await _employeeService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Data.EmployeeId }, result);
            }

            // PUT: api/employee
            [HttpPut]
            public async Task<IActionResult> Update([FromBody] EmployeeDto dto)
            {
                var result = await _employeeService.UpdateAsync(dto);
                return result.IsSuccess ? Ok(result) : NotFound(result);
            }

            // DELETE: api/employee/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var result = await _employeeService.DeleteAsync(id);
                return result.IsSuccess ? Ok(result) : NotFound(result);
            }
        }
 }


