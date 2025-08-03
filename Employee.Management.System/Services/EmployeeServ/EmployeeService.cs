using AutoMapper;
using Employee.Management.System.Data;
using Employee.Management.System.DTOS;
using Employee.Management.System.Exceptions;
using Employee.Management.System.Models;
using Employee.Management.System.Repositories;
using Employee.Management.System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.System.Services.EmployeeServ
{



    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employe> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<Employe> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
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
            var query = _employeeRepository.GetAll();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            if (departmentId.HasValue)
                query = query.Where(e => e.DepartmentId == departmentId);

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<EmployeeStatus>(status, true, out var parsedStatus))
                query = query.Where(e => e.Status == parsedStatus);

            if (hireDateFrom.HasValue)
                query = query.Where(e => e.HireDate >= hireDateFrom.Value);

            if (hireDateTo.HasValue)
                query = query.Where(e => e.HireDate <= hireDateTo.Value);

            query = sortBy switch
            {
                "name" => isAscending ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name),
                "hireDate" => isAscending ? query.OrderBy(e => e.HireDate) : query.OrderByDescending(e => e.HireDate),
                _ => query
            };

            // Department include is needed, but since IRepository doesn’t support Include,
            // you can either enhance IRepository or project manually if needed.

            var result = await query.ToListAsync();

            var dtoList = _mapper.Map<List<EmployeeDto>>(result);
            return ResultViewModel<List<EmployeeDto>>.Sucess(dtoList, "Employees fetched successfully");
        }

        public async Task<ResultViewModel<EmployeeDto>> GetByIdAsync(int id)
        {
            var emp = await Task.FromResult(_employeeRepository.GetByID(id));
            if (emp == null)
                return ResultViewModel<EmployeeDto>.Faliure(ErrorCode.NotFound, "Employee not found");

            return ResultViewModel<EmployeeDto>.Sucess(_mapper.Map<EmployeeDto>(emp), "Employee retrieved");
        }

        public async Task<ResultViewModel<EmployeeDto>> CreateAsync(EmployeeDto dto)
        {
            var emp = _mapper.Map<Employe>(dto);

            _employeeRepository.Add(emp);
            _employeeRepository.SaveChanges();

            return ResultViewModel<EmployeeDto>.Sucess(_mapper.Map<EmployeeDto>(emp), "Employee created successfully");
        }

        public async Task<ResultViewModel<EmployeeDto>> UpdateAsync(EmployeeDto dto)
        {
            var emp = _employeeRepository.GetWithTrackinByID(dto.EmployeeId);
            if (emp == null)
                return ResultViewModel<EmployeeDto>.Faliure(ErrorCode.NotFound, "Employee not found");

            _mapper.Map(dto, emp);
            _employeeRepository.Update(emp);
            _employeeRepository.SaveChanges();

            return ResultViewModel<EmployeeDto>.Sucess(_mapper.Map<EmployeeDto>(emp), "Employee updated successfully");
        }


        public async Task<ResultViewModel<bool>> DeleteAsync(int id)
        {
            var emp = _employeeRepository.GetByID(id);
            if (emp == null)
                return ResultViewModel<bool>.Faliure(ErrorCode.NotFound, "Employee not found");

            _employeeRepository.Delete(emp);
            _employeeRepository.SaveChanges();

            return ResultViewModel<bool>.Sucess(true, "Employee deleted successfully");
        }




    }



}
