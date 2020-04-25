using Raven.Client.Documents.Session;
using System.Threading.Tasks;

namespace RavenDbPersistenceConsole.Data
{
    public class RavenDbUnitOfWork : IRavenDbUnitOfWork
    {
        private readonly IAsyncDocumentSession _session;
        public RavenDbUnitOfWork(IAsyncDocumentSession session)
        => _session = session;
        public Task Commit() => _session.SaveChangesAsync();
    }
}
