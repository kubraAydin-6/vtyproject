using FreKE.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FreKE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _logRepository;

        public LogController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogTotals()
        {
            var log = await _logRepository.GetLogTotalAsync();
            return Ok(log);
        }
    }
}
