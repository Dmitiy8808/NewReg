using System.Text.Json;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Repository;
using Entities.RequestFeatures;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/regrequests")]
    [ApiController]
    public class RegRequestsController : ControllerBase
    {
        private readonly IQualifiedCertificateManager _qualifiedCertificateManager;
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;
        public RegRequestsController(IQualifiedCertificateManager qualifiedCertificateManager, 
                IRequestRepository requestRepository, IMapper mapper)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
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

        [Route("RequestAbonent")]
        [HttpPost]
        public async Task<IActionResult> CreateRegRequestAbonent(RequestAbonentCreateDto requestAbonentCreateDto)
        {
            var requestAbonentModel = _mapper.Map<RequestAbonent>(requestAbonentCreateDto);
            
            await _requestRepository.CreateRequest(requestAbonentModel);

            var requestAbonentReadeDto = _mapper.Map<RequestAbonentReadDto>(requestAbonentModel);

            return CreatedAtAction(nameof(GetRegRequestAbonent), new { id = requestAbonentReadeDto.Id }, requestAbonentReadeDto);
        }

        [Route("RequestAbonent/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult<RequestAbonentReadDto>>   GetRegRequestAbonent(Guid id)
        {
            var requestAbonent = await _requestRepository.GetRequest(id);
            if (requestAbonent != null)
            {
                return Ok(_mapper.Map<RequestAbonentReadDto>(requestAbonent));
            }
            return NotFound();
        }

        [Route("RequestAbonent")]
        [HttpGet]
        public async Task<IActionResult> GetRegRequestAbonents([FromQuery] RequestAbonentParameters requestAbonentParameters)
        {
            var requestAbonents = await _requestRepository.GetRequests(requestAbonentParameters);

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