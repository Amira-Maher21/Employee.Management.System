using AutoMapper;
using Employee.Management.System.DTOS;
using Employee.Management.System.Exceptions;
using Employee.Management.System.mediator;
using Employee.Management.System.Models;
using Employee.Management.System.Paginated;
using Employee.Management.System.Services.DepartmentServ;
using Employee.Management.System.UnitOfWork;
using Employee.Management.System.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.System.Services.EmployeeServ
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IDepartmentService _departmentService;

        public EmployeeService(IUnitOfWork unitOfWork,
                               IMapper mapper,
                               IDepartmentService departmentService,
                               IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _departmentService = departmentService;
            _mediator = mediator;
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
            var query = _unitOfWork.Employees.GetAll();

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

            var result = await query.ToListAsync();
            var dtoList = _mapper.Map<List<EmployeeDto>>(result);

            return ResultViewModel<List<EmployeeDto>>.Sucess(dtoList, "Employees fetched successfully");
        }

        public async Task<ResultViewModel<EmployeeDto>> GetByIdAsync(int id)
        {
            var emp = await Task.FromResult(_unitOfWork.Employees.GetByID(id));
            if (emp == null)
                return ResultViewModel<EmployeeDto>.Faliure(ErrorCode.NotFound, "Employee not found");

            return ResultViewModel<EmployeeDto>.Sucess(_mapper.Map<EmployeeDto>(emp), "Employee retrieved");
        }

        public async Task<ResultViewModel<EmployeeDto>> CreateAsync(EmployeeDto dto)
        {
            var emp = _mapper.Map<Employe>(dto);

            _unitOfWork.Employees.Add(emp);
            _unitOfWork.SaveChanges();

            await _mediator.Send(new LogEmployeeActionCommand("Create", emp.ID, emp.Name));

            return ResultViewModel<EmployeeDto>.Sucess(_mapper.Map<EmployeeDto>(emp), "Employee created successfully");
        }

        public async Task<ResultViewModel<EmployeeDto>> UpdateAsync(EmployeeDto dto)
        {
            var emp = _unitOfWork.Employees.GetWithTrackinByID(dto.EmployeeId);
            if (emp == null)
                return ResultViewModel<EmployeeDto>.Faliure(ErrorCode.NotFound, "Employee not found");

            _mapper.Map(dto, emp);
            _unitOfWork.Employees.Update(emp);
            _unitOfWork.SaveChanges();

            await _mediator.Send(new LogEmployeeActionCommand("Update", emp.ID, emp.Name));

            return ResultViewModel<EmployeeDto>.Sucess(_mapper.Map<EmployeeDto>(emp), "Employee updated successfully");
        }

        public async Task<ResultViewModel<bool>> DeleteAsync(int id)
        {
            var emp = _unitOfWork.Employees.GetByID(id);
            if (emp == null)
                return ResultViewModel<bool>.Faliure(ErrorCode.NotFound, "Employee not found");

            _unitOfWork.Employees.Delete(emp);
            _unitOfWork.SaveChanges();

            await _mediator.Send(new LogEmployeeActionCommand("Delete", emp.ID, emp.Name));

            return ResultViewModel<bool>.Sucess(true, "Employee deleted successfully");
        }

        public async Task<PaginatedResult<EmployeeDto>> GetEmployeesPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Employees.GetAll();

            var totalCount = await query.CountAsync();
            var employees = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PaginatedResult<EmployeeDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = _mapper.Map<List<EmployeeDto>>(employees)
            };

            return result;
        }
    }
}
