using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Repository;

namespace Server.Controllers
{
    [Route("api/country")]
    [ApiController]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry(Country country)
        {
            await _countryRepository.CreateCountry(country);

            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
        }

        [Route("createList")]
        [HttpPost]
        public async Task<IActionResult> CreateCountryList(List<CountryReadDto> countries)
        {
            var coyntryList = _mapper.Map<List<Country>>(countries);
            
            await _countryRepository.CreateCountries(coyntryList); 

            return Ok(); //Ответ должен быть 201 Cerated с указанием созданных Id
        }

        [Route("{id:guid}")]
        [HttpGet]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _countryRepository.GetCountry(id); 
            if (country != null)
            {
                return Ok(country);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            var countries = await _countryRepository.GetCountries();

            return Ok(countries);
        }
        
    }
}