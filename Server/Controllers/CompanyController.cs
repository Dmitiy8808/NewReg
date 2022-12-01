using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Repository;

namespace Server.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyCreateDto company)
        {
            var requestCompanyModel = _mapper.Map<Company>(company);
            
            await _companyRepository.CreateCompany(requestCompanyModel);

            var requestCompanyReadeDto = _mapper.Map<CompanyReadDto>(requestCompanyModel);

            return CreatedAtAction(nameof(GetCompany), new { id = requestCompanyModel.Id }, requestCompanyReadeDto);

        }

        [Route("{id:guid}")]
        [HttpGet]
        public async Task<ActionResult<CompanyReadDto>> GetCompany(Guid id)
        {
            var company = await _companyRepository.GetCompany(id);
            if (company != null)
            {
                return Ok(_mapper.Map<CompanyReadDto>(company));
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<ActionResult<List<CompanyReadDto>>> GetGetCompanies()
        {
            var companies = await _companyRepository.GetCompanies();

            return Ok(_mapper.Map<List<CompanyReadDto>>(companies));
        }
    }
}