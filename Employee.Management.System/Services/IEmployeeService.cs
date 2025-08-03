using Employee.Management.System.DTOS;
using Employee.Management.System.ViewModels;

namespace Employee.Management.System.Services
{
    public interface IEmployeeService
    {
        Task<ResultViewModel<List<EmployeeDto>>> GetAllAsync(string? name = null, int? departmentId = null, string? status = null, DateTime? hireDateFrom = null, DateTime? hireDateTo = null, string? sortBy = null, bool isAscending = true);
         Task<ResultViewModel<EmployeeDto>> GetByIdAsync(int id);
        Task<ResultViewModel<EmployeeDto>> CreateAsync(EmployeeDto dto);
        Task<ResultViewModel<EmployeeDto>> UpdateAsync(EmployeeDto dto);
        Task<ResultViewModel<bool>> DeleteAsync(int id);
    }
}
