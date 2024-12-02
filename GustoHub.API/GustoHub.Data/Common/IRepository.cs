namespace GustoHub.Data.Common
{
    public interface IRepository
    {
        Task AddAsync<T>(T entity) where T : class;     
        Task<T?> GetByIdAsync<T>(object id) where T : class;
        Task<T?> GetByIdsAsync<T>(object firstId, object secondId) where T : class;
        Task<List<T>> AllAsync<T>() where T : class;
        IQueryable<T> AllAsReadOnly<T>() where T : class;
        Task RemoveAsync<T>(object id) where T : class;
        Task<int> SaveChangesAsync();
    }
}
