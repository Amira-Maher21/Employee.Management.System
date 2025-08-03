using AutoMapper;
using Employee.Management.System.Data;
using Employee.Management.System.DTOS;
using Employee.Management.System.Exceptions;
using Employee.Management.System.Models;
using Employee.Management.System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.System.Services
{


    
        public class EmployeeService : IEmployeeService
        {
            private readonly StoreContext _context;
            private readonly IMapper _mapper;

            public EmployeeService(StoreContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ResultViewModel<List<EmployeeDto>>> GetAllAsync(
                string? name = null,
                int? departmentId = null,
                string? status = null,
                DateTime? hireDateFrom = null,
                DateTime? hireDateTo = null,
                string? sortBy = null,
                bool isAscending = true)
            {
                var query = _context.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                    query = query.Where(e => e.Name.Contains(name));

                if (departmentId.HasValue)
                    query = query.Where(e => e.DepartmentId == departmentId);

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<EmployeeStatus>(status, true, out var parsedStatus))
            {
                query = query.Where(e => e.Status == parsedStatus);
            }

            if (hireDateFrom.HasValue)
                    query = query.Where(e => e.HireDate >= hireDateFrom.Value);

                if (hireDateTo.HasValue)
                    query = query.Where(e => e.HireDate <= hireDateTo.Value);

                // Sorting
                query = sortBy switch
                {
                    "name" => isAscending ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name),
                    "hireDate" => isAscending ? query.OrderBy(e => e.HireDate) : query.OrderByDescending(e => e.HireDate),
                    _ => query
                };

                var result = await query
                    .Include(e => e.Department)
                    .ToListAsync();

                var dtoList = _mapper.Map<List<EmployeeDto>>(result);
                return ResultViewModel<List<EmployeeDto>>.Sucess(dtoList, "Employees fetched successfully");
            }

            public async Task<ResultViewModel<EmployeeDto>> GetByIdAsync(int id)
            {
                var emp = await _context.Employees.FindAsync(id);
                if (emp == null)
                    return ResultViewModel<EmployeeDto>.Faliure(ErrorCode.NotFound, "Employee not found");

                return ResultViewModel<EmployeeDto>.Sucess(_mapper.Map<EmployeeDto>(emp), "Employee retrieved");
            }

            public async Task<ResultViewModel<EmployeeDto>> CreateAsync(EmployeeDto dto)
            {
                var emp = _mapper.Map<Employe>(dto);

                await _context.Employees.AddAsync(emp);
                await _context.SaveChangesAsync();

                return ResultViewModel<EmployeeDto>.Sucess(_mapper.Map<EmployeeDto>(emp), "Employee created successfully");
            }

            public async Task<ResultViewModel<EmployeeDto>> UpdateAsync(EmployeeDto dto)
            {
                var emp = await _context.Employees.FindAsync(dto.EmployeeId);
                if (emp == null)
                    return ResultViewModel<EmployeeDto>.Faliure(ErrorCode.NotFound, "Employee not found");

                _mapper.Map(dto, emp);
                _context.Employees.Update(emp);
                await _context.SaveChangesAsync();

                return ResultViewModel<EmployeeDto>.Sucess(_mapper.Map<EmployeeDto>(emp), "Employee updated successfully");
            }

            public async Task<ResultViewModel<bool>> DeleteAsync(int id)
            {
                var emp = await _context.Employees.FindAsync(id);
                if (emp == null)
                    return ResultViewModel<bool>.Faliure(ErrorCode.NotFound, "Employee not found");

                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();

                return ResultViewModel<bool>.Sucess(true, "Employee deleted successfully");
            }

        

         
    }
    


}
