using AutoMapper;
using Employee.Management.System.Data;
using Employee.Management.System.DTOS;
using Employee.Management.System.Models;
using Employee.Management.System.Repositories;
using Employee.Management.System.Services.DepartmentServ;
using Employee.Management.System.Services.EmployeeServ;
using Employee.Management.System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.System.Services.DepartmentServ
{
     public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IRepository<Department> departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<ResultViewModel<List<DepartmentDto>>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAll().ToListAsync();
            var dtoList = _mapper.Map<List<DepartmentDto>>(departments);

            return ResultViewModel<List<DepartmentDto>>.Sucess(dtoList, "Departments fetched successfully");
        }


        public async Task<ResultViewModel<DepartmentDto>> AddDepartmentAsync(DepartmentDto dto)
        {
            var department = _mapper.Map<Department>(dto);
            _departmentRepository.Add(department);
            _departmentRepository.SaveChanges();

            var resultDto = _mapper.Map<DepartmentDto>(department);
            return ResultViewModel<DepartmentDto>.Sucess(resultDto, "Department created successfully");
        }



    }
}


