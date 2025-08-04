namespace ProjectForTesting.Repositories;

public interface IRepository<T>
{
    IQueryable<T> GetQueryable();
    Task<T> InsertAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(IEnumerable<T> entities);
}