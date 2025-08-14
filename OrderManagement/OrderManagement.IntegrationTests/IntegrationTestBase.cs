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
