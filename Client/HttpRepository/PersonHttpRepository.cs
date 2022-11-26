using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entities.Models;

namespace Reg.Client.HttpRepository
{
    public class PersonHttpRepository : IPersonHttpRepository
    {
        private readonly HttpClient _client;
        public PersonHttpRepository( HttpClient client)
        {
            _client = client;
        }

        public async Task CreatePerson(Person person)
        {
            await _client.PostAsJsonAsync("persons", person);
        }

        public async Task<Person> GetPerson(int id)
        {
            var person = await _client.GetFromJsonAsync<Person>($"persons/{id}");
            return person;
        }

        public async Task<List<Person>> GetPersons()
        {
            var persons = await _client.GetFromJsonAsync<List<Person>>("persons");
            return persons;
        }
    }
}