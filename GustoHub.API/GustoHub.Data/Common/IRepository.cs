﻿namespace GustoHub.Data.Common
{
    public interface IRepository
    {
        Task AddAsync<T>(T entity) where T : class;     
        Task<T?> GetByIdAsync<T>(object id) where T : class;
        IQueryable<T> All<T>() where T : class;
        IQueryable<T> AllAsReadOnly<T>() where T : class;
        void Remove<T>(T entity) where T : class;
        void RemoveRange<T>(IEnumerable<T> entities) where T : class;
        Task<int> SaveChangesAsync();
    }
}
