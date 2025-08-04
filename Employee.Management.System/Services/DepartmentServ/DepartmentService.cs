using AutoMapper;
using Employee.Management.System.DTOS;
using Employee.Management.System.Models;
using Employee.Management.System.UnitOfWork;
using Employee.Management.System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.System.Services.DepartmentServ
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultViewModel<List<DepartmentDto>>> GetAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.Departments.GetAll().ToListAsync();
            var dtoList = _mapper.Map<List<DepartmentDto>>(departments);

            return ResultViewModel<List<DepartmentDto>>.Sucess(dtoList, "Departments fetched successfully");
        }

        public async Task<ResultViewModel<DepartmentDto>> AddDepartmentAsync(DepartmentDto dto)
        {
            var department = _mapper.Map<Department>(dto);
            _unitOfWork.Departments.Add(department);
            await _unitOfWork.CompleteAsync();

            var resultDto = _mapper.Map<DepartmentDto>(department);
            return ResultViewModel<DepartmentDto>.Sucess(resultDto, "Department created successfully");
        }
    }
}
