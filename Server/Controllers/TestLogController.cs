using Microsoft.AspNetCore.Mvc;
using Server.Contracts;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestLogController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        public TestLogController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("Info message");
            _logger.LogError("Error message");
            _logger.LogDebug("Debug message");
            _logger.LogWarn("Warn message");

            return new string [] {"value1", "value2"};
        }
    }
}