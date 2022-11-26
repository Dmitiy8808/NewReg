using Entities.Models;

namespace Server.Repository
{
    public interface IPersonRepository
    {
        Task<Person> GetPersons();
        Task<Person> GetPerson(int id);
        Task CreatePerson(Person person);
    
    }
}