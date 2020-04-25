using RavenDbPersistenceConsole.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RavenDbPersistenceConsole.Services
{
    public interface IPersonService
    {
        Task Create(Person entity);
        Task Delete(int id);
        Task<Person> Get(int id);
        Task<IEnumerable<Person>> GetAll();
        Task<int> Count();
    }
}