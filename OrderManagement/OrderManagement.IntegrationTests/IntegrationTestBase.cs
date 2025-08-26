using Xunit.Sdk;

namespace OrderManagement.IntegrationTests
{
    public abstract class IntegrationTestBase : IDisposable
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IFixture _fixture;

        protected IntegrationTestBase()
        {
            _serviceProvider = TestStartup.ConfigureServices();

            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Returns new InMemory db & controller which uses db
        /// </summary>
        protected (InMemoryTestDatabase Db, TController Controller) CreateController<TController, TService, TRepo>()
            where TController : class
            where TService : class
            where TRepo : class
        {
            var db = new InMemoryTestDatabase();
            var repo = (TRepo)Activator.CreateInstance(typeof(TRepo), db)!;
            var service = (TService)Activator.CreateInstance(typeof(TService), repo)!;
            var controller = (TController)Activator.CreateInstance(typeof(TController), service)!;

            return (db, controller);
        }

        protected TController CreateController<TController, TService, TRepo1, TRepo2>(InMemoryTestDatabase db)
    where TController : class
    where TService : class
    where TRepo1 : class
    where TRepo2 : class
        {
            var repo1 = (TRepo1)Activator.CreateInstance(typeof(TRepo1), db)!;
            var repo2 = (TRepo2)Activator.CreateInstance(typeof(TRepo2), db)!;
            var service = (TService)Activator.CreateInstance(typeof(TService), repo1, repo2)!;
            var controller = (TController)Activator.CreateInstance(typeof(TController), service)!;

            return controller;
        }

        protected T GetService<T>() where T : notnull
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        public virtual void Dispose()
        {
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
