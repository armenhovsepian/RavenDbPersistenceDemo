using RavenDbPersistenceConsole.Data;
using RavenDbPersistenceConsole.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RavenDbPersistenceConsole.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRavenDbUnitOfWork _unitOfWork;
        private readonly IPersonRepository _repository;

        public PersonService(IRavenDbUnitOfWork ravenDbUnitOfWork, IPersonRepository personRepository)
        {
            _unitOfWork = ravenDbUnitOfWork;
            _repository = personRepository;
        }

        public async Task<Person> Get(int id)
            => await _repository.Get(id);
        

        public async Task Create(Person entity)
        {
            if (await _repository.Exists(entity.Id))
                throw new InvalidOperationException(
                $"Entity with id {entity.Id} already exists");

            await _repository.Create(entity);
            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Person>> GetAll()
           => await _repository.GetAll();

        public async Task<int> Count()
            => await _repository.Count();

    }
}
