using System.Text.Json;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Repository;
using Entities.RequestFeatures;
using Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Server.Data;

namespace Server.Controllers
{
    [Route("api/regrequests")]
    [ApiController]
    [Authorize]
    public class RegRequestsController : ControllerBase
    {
        
        private readonly UserManager<User> _userManager;
        private readonly IQualifiedCertificateManager _qualifiedCertificateManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;
        private readonly IRegistrationService _registrationService;
        private readonly IFileRepository _fileRepository;
        public RegRequestsController(IQualifiedCertificateManager qualifiedCertificateManager, 
        
                IRequestRepository requestRepository, IMapper mapper, IRegistrationService registrationService,
                IAuthorizationService authorizationService, UserManager<User> userManager, IFileRepository fileRepository)
        {
            _registrationService = registrationService;
            _mapper = mapper;
            _requestRepository = requestRepository;
            _qualifiedCertificateManager = qualifiedCertificateManager;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _fileRepository = fileRepository;
        }

        [Route("getRequestData")]
        [HttpPost]
        public async Task<IActionResult> GetCertRequestData(RequestAbonent clientAbonent)
        {
            var certRequestData = await _qualifiedCertificateManager.GetCertificateRequestData(clientAbonent);

            return Ok(certRequestData);
        }

        [Route("getCertificateData/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult<CertificateDataDto>> GetCertificatetData(Guid id)
        {
            var requestData = await _requestRepository.GetRequest(id);
            var certData = await _fileRepository.GetCertificateFileByRequestId(id);
            CertificateDataDto response = new CertificateDataDto();
            response.IsError = false;
            response.ProviderCode = int.Parse(requestData.Person.CryptoProviderCode);
            response.ProviderName = requestData.Person.CryptoProviderName;
            response.ContainerName = requestData.ContainerName;
            response.CertData = Convert.ToBase64String(certData.Data);

            return Ok(response);
        }

       

        [Route("createRequest")]
        [HttpPost]
        public async Task<IActionResult> CreateRegRequest(RequestAbonent clientAbonent)
        {
            var certRequestData = await _qualifiedCertificateManager.GetCertificateRequestData(clientAbonent);

            return Ok(certRequestData);
        }

        [Route("RequestAbonent")]
        [HttpPost]
        public async Task<IActionResult> CreateRegRequestAbonent(RequestAbonentCreateDto requestAbonentCreateDto)
        {
            var requestAbonentModel = _mapper.Map<RequestAbonent>(requestAbonentCreateDto);
            
            await _requestRepository.CreateRequest(requestAbonentModel);

            var requestAbonentReadeDto = _mapper.Map<RequestAbonentReadDto>(requestAbonentModel);

            return CreatedAtAction(nameof(GetRegRequestAbonent), new { id = requestAbonentReadeDto.Id }, requestAbonentReadeDto);
        }

        [Route("RequestAbonentList")]
        [HttpPost]
        public async Task<IActionResult> CreateRegRequestAbonentList(RequestAbonentListDto requestAbonentCreateDtoList)
        {
            var requestAbonentModelList = _mapper.Map<List<RequestAbonent>>(requestAbonentCreateDtoList.AbonentList);
            
            await _requestRepository.CreateRequests(requestAbonentModelList);
            await _registrationService.CreateIdentityUsersFromRequestAbonentList(requestAbonentCreateDtoList);

            return Ok(); //Ответ должен быть 201 Ceratedc с указанием созданных Id
        }

        [Route("RequestAbonent/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult<RequestAbonentReadDto>> GetRegRequestAbonent(Guid id)
        {
            var requestAbonent = await _requestRepository.GetRequest(id);
            if (requestAbonent != null)
            {
                return Ok(_mapper.Map<RequestAbonentReadDto>(requestAbonent));
            }
            return NotFound();
        }

        [Route("DraftRequestAbonent")]
        [HttpGet]
        public async Task<IActionResult> GetDraftRegRequestAbonents([FromQuery] RequestAbonentParameters requestAbonentParameters)
        {
             IEnumerable<RequestAbonent>? queryRequestAbonent = new List<RequestAbonent>();
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var login = HttpContext.User.Identity.Name;
            var adminRole = await _userManager.IsInRoleAsync(user, "Administrator");
            var appAdminRole = await _userManager.IsInRoleAsync(user, "AppAdministrator");
            var requestAbonents = await _requestRepository.GetDraftRequests(requestAbonentParameters);

            if (!adminRole)
            {
                if (appAdminRole)
                {
                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(requestAbonents.MetaData));
                    return Ok(requestAbonents);
                }
                queryRequestAbonent = requestAbonents.Where(x => x.Person.Email == login);
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(requestAbonents.MetaData));
                return Ok(queryRequestAbonent);
            }
            

           
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(requestAbonents.MetaData));
            return Ok(requestAbonents);
        }

        [Route("RequestAbonent")]
        [HttpGet]
        public async Task<IActionResult> GetRegRequestAbonents([FromQuery] RequestAbonentParameters requestAbonentParameters)
        {
            IEnumerable<RequestAbonent>? queryRequestAbonent = new List<RequestAbonent>();
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var login = HttpContext.User.Identity.Name;
            var adminRole = await _userManager.IsInRoleAsync(user, "Administrator");
            var appAdminRole = await _userManager.IsInRoleAsync(user, "AppAdministrator");
            var requestAbonents = await _requestRepository.GetRequests(requestAbonentParameters);

            if (!adminRole)
            {   
                if (appAdminRole)
                {
                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(requestAbonents.MetaData));
                    return Ok(requestAbonents);
                }
                
                queryRequestAbonent = requestAbonents.Where(x => x.Person.Email == login);
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(requestAbonents.MetaData));
                return Ok(queryRequestAbonent);
            }

           
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(requestAbonents.MetaData));
            return Ok(requestAbonents);

            
        }

        [Route("RequestAbonent/{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> UpdateRegRequestAbonent(Guid id, RequestAbonentUpdateDto requestAbonentUpdateDto)
        {
            var clientAbonentFromRepo = await _requestRepository.GetRequest(id);
            if(clientAbonentFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(requestAbonentUpdateDto, clientAbonentFromRepo);

            _requestRepository.UpdateRequest(clientAbonentFromRepo);

            await _requestRepository.SaveAsync();

            return NoContent();
        }

        [Route("RequestAbonent/{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRegRequestAbonent(Guid id)
        {
            var clientAbonentFromRepo = await _requestRepository.GetRequest(id);
            if(clientAbonentFromRepo == null)
            {
                return NotFound();
            }

            await _requestRepository.DeleteRequest(clientAbonentFromRepo);

            return NoContent();
        }

    }
}