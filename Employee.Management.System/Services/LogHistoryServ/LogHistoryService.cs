using AutoMapper;
using Employee.Management.System.DTOS;
using Employee.Management.System.mediator;
using Employee.Management.System.Models;
using Employee.Management.System.Repositories;
using Employee.Management.System.Services.EmployeeServ;
using Employee.Management.System.ViewModels;
using MediatR;

namespace Employee.Management.System.Services.LogHistoryServ
{

    public class LogHistoryService : ILogHistoryService
    {
        private readonly IRepository<LogHistory> _LogHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public LogHistoryService(IRepository<LogHistory> LogHistoryRepository, IMapper mapper, IMediator mediator)
        {
            _LogHistoryRepository = LogHistoryRepository;
            _mapper = mapper;
            _mediator= mediator;

        }



        public async Task<ResultViewModel<List<LogHistoryDto>>> GetAllLogsAsync()
        {
            var logs = _LogHistoryRepository.GetAll().ToList();

            if (logs == null || !logs.Any())
            {
                return ResultViewModel<List<LogHistoryDto>>.Faliure(
                    Exceptions.ErrorCode.NotFound, "No log history found.");
            }

            var logsDto = _mapper.Map<List<LogHistoryDto>>(logs);

            return ResultViewModel<List<LogHistoryDto>>.Sucess(logsDto, "Log history retrieved successfully.");
        }





    }

}
