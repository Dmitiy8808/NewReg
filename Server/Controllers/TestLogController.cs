using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Contracts;
using Server.Services.EmailService;

namespace Server.Controllers
{
    [Route("api/test")]
    [ApiController]
    [Authorize]
    public class TestLogController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        public TestLogController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
            
        }
        [HttpGet]
        public void Get()
        {
            var message = new EmailMessage(new string[] { "dmitriy-stupin@mail.ru" }, "Test email", "This is the content from our email.");
            _emailSender.SendEmail(message);

        }

    }
}