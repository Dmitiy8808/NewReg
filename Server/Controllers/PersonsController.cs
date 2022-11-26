using System.Text.Json;
using Reg.Server.Repository;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Server.Repository;

namespace Reg.Server.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _repo;

        public PersonsController(IPersonRepository repo)
        {
            _repo = repo;
        }

		// [HttpGet]
        // public async Task<IActionResult> Get([FromQuery]ProductParameters productParameters)
        // {
        //     var products = await _repo.GetProducts(productParameters);

        //     Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(products.MetaData));

        //     return Ok(products);
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var product = await _repo.GetPerson(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody]Person person)
        {
            await _repo.CreatePerson(person);
            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);

        }
    }
}