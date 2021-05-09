using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alms.cherry.data.objects.Infrastructure;
using Npgsql;

namespace alms.cherry.data.process.Infrastructure
{
    public interface IRepository
    {
        void Attach(IUnitOfWork unitOfWork);
    }

    public interface IRepository<TEntity> : IRepository where TEntity : class
    {
        Task<TResult> Add<TResult>(TEntity entity);
        Task<int> AddAll<TResult>(IEnumerable<TEntity> entities);
        Task<int> Delete(object id);
        Task<int> Delete(TEntity entity);
        Task<TResult> Merge<TResult>(TEntity entity);
        Task<TEntity> Query(int id);
        Task<TEntity> Query(Guid id);
        Task<int> Update(TEntity entity);
    }
}