using Employee.Management.System.DTOS;
using Employee.Management.System.ViewModels;
using Employee.Management.System.Services.DepartmentServ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Employee.Management.System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/department
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _departmentService.GetAllDepartmentsAsync();
            return Ok(ResultViewModel<List<DepartmentDto>>.Sucess(result));
        }

        // POST: api/department
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResultViewModel<string>.Faliure(
                    Exceptions.ErrorCode.ValidationError, "Invalid model data"));
            }

            var result = await _departmentService.AddDepartmentAsync(dto);
            return Ok(ResultViewModel<DepartmentDto>.Sucess(result, "Department added successfully"));
        }
    }
}
