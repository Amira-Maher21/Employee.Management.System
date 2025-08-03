using Employee.Management.System.DTOS;
using Employee.Management.System.ViewModels;

namespace Employee.Management.System.Services.DepartmentServ
{
    public interface IDepartmentService
    {
      
    
            Task<ResultViewModel<List<DepartmentDto>>> GetAllDepartmentsAsync();
            Task<ResultViewModel<DepartmentDto>> AddDepartmentAsync(DepartmentDto dto);
    }
}

