namespace ProjectForTesting.Repositories;

public class InMemoryRepository<T> : IRepository<T> where T : class, new()
{
    private readonly List<T> _data = [];

    public IQueryable<T> GetQueryable() => _data.AsQueryable();

    public Task<T> InsertAsync(T entity)
    {
        _data.Add(entity);
        return Task.FromResult(entity);
    }

    public Task DeleteAsync(T entity)
    {
        _data.Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        foreach (var e in entities.ToList())
            _data.Remove(e);
        return Task.CompletedTask;
    }
}