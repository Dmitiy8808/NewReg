using Entities.Models;

namespace Reg.Client.HttpRepository
{
    public interface IPersonHttpRepository
    {
        Task<List<Person>> GetPersons();
        Task<Person> GetPerson(int id);
        Task CreatePerson(Person product);

    }
}