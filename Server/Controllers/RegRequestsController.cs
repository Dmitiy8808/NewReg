using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/regrequests")]
    [ApiController]
    public class RegRequestsController : ControllerBase
    {
        private readonly IQualifiedCertificateManager _qualifiedCertificateManager;
        public RegRequestsController(IQualifiedCertificateManager qualifiedCertificateManager)
        {
            _qualifiedCertificateManager = qualifiedCertificateManager;
            
        }

        [Route("getRequestData")]
        [HttpPost]
        public async Task<IActionResult> GetCertRequestData(RequestAbonent clientAbonent)
        {
            var certRequestData = await _qualifiedCertificateManager.GetCertificateRequestData(clientAbonent);

            return Ok(certRequestData);
        }

        [Route("createRequest")]
        [HttpPost]
        public async Task<IActionResult> CreateRegRequest(RequestAbonent clientAbonent)
        {
            var certRequestData = await _qualifiedCertificateManager.GetCertificateRequestData(clientAbonent);

            return Ok(certRequestData);
        }
    }
}