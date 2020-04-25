using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using RavenDbPersistenceConsole.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RavenDbPersistenceConsole.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IAsyncDocumentSession _session;

        public PersonRepository(IAsyncDocumentSession session)
           => _session = session;
        

        public async Task Create(Person entity)
            => await _session.StoreAsync(entity, EntityId(entity.Id));

        public async Task<bool> Exists(int id)
            => await _session.Advanced.ExistsAsync(EntityId(id));

        public async Task<Person> Get(int id)
            => await _session.LoadAsync<Person>(EntityId(id));

        private static string EntityId(int id)
            => $"people/{id.ToString()}";

        public async Task<IEnumerable<Person>> GetAll()
            => await _session.Query<Person>().ToListAsync();

        public async Task Delete(int id)
        {
            var entity = await Get(id);
            _session.Delete(entity);
        }

        public async Task<int> Count()
            => await _session.Query<Person>().CountAsync();
        
    }
}
