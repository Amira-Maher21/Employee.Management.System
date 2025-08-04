using Employee.Management.System.DTOS;
using Employee.Management.System.ViewModels;

namespace Employee.Management.System.Services.LogHistoryServ
{
    public interface ILogHistoryService
    {
        Task<ResultViewModel<List<LogHistoryDto>>> GetAllLogsAsync();

    }
}
