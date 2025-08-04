using Employee.Management.System.Services.LogHistoryServ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Employee.Management.System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogHistoryController : ControllerBase
    {
        private readonly ILogHistoryService _logHistoryService;

        public LogHistoryController(ILogHistoryService logHistoryService)
        {
            _logHistoryService = logHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            var result = await _logHistoryService.GetAllLogsAsync();

            if (!result.IsSuccess)
                return NotFound(result); // أو ممكن return BadRequest(result) حسب نوع الخطأ

            return Ok(result);
        }
    }
}
