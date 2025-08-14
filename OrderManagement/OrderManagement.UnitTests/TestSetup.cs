namespace OrderManagement.UnitTests
{
    public class TestSetup
    {
        protected readonly IFixture _fixture;

        public TestSetup()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
