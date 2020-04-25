using RavenDbPersistenceConsole.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RavenDbPersistenceConsole.Data
{
    public interface IPersonRepository
    {
        Task Create(Person entity);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<Person> Get(int id);
        Task<IEnumerable<Person>> GetAll();

        Task<int> Count();
    }
}