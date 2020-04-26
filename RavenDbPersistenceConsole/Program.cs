using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using RavenDbPersistenceConsole.Data;
using RavenDbPersistenceConsole.Models;
using RavenDbPersistenceConsole.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RavenDbPersistenceConsole
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static async Task Main(string[] args)
        {
            // Create service collection
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            var personService = serviceProvider.GetService<IPersonService>();

            var count = await personService.Count();
            if (count == 0)
                for (int id = 1; id =< 10; id++)
                    await personService.Create(new Person { Id = id, FirstName = $"FirstName {id}", LastName = $"LastName {id}" });
                
            var persons = await personService.GetAll();
            foreach (var person in persons)
                Console.WriteLine(person.FirstName + "\t" + person.LastName);
            

            // Ok
            //var person = await personService.Get(3);

            // Ok
            //await personService.Delete(3);

        }



        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            configuration = builder.Build();

            var ravenDbSettings = configuration.GetSection("Database").Get<AppSettings.DatabaseSettings>();

            var store = new DocumentStore
            {
                Urls = ravenDbSettings.Urls,
                Database = ravenDbSettings.DatabaseName,
                Conventions =
                    {
                        FindIdentityProperty = m => m.Name == "_databaseId"
                    }
            };

            store.Initialize();

            serviceCollection
                .AddSingleton(c => store.OpenAsyncSession())
                .AddSingleton<IRavenDbUnitOfWork, RavenDbUnitOfWork>()
                .AddSingleton<IPersonRepository, PersonRepository>()
                .AddSingleton<IPersonService, PersonService>()
                .BuildServiceProvider();

        }

    }

}
