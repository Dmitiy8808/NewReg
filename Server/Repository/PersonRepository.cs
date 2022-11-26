using Entities.Models;
using Reg.Server.Context;

namespace Server.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly RegContext _context;
        public PersonRepository(RegContext context)
        {
            _context = context;
        }
        public async Task CreatePerson(Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> GetPerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            return person;
        }

        public Task<Person> GetPersons()
        {
            throw new NotImplementedException();
        }
    }
}