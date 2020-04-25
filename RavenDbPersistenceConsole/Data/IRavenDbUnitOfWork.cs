using System.Threading.Tasks;

namespace RavenDbPersistenceConsole.Data
{
    public interface IRavenDbUnitOfWork
    {
        Task Commit();
    }
}