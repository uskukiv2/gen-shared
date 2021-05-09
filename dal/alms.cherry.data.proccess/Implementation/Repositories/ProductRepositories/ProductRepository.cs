using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alms.cherry.data.objects;
using alms.cherry.data.objects.Entities.Employees;
using alms.cherry.data.objects.Entities.Product;
using alms.cherry.data.process.Infrastructure;
using alms.cherry.data.process.Interfaces;
using alms.cherry.infra.infrastructure;
using Npgsql;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Interfaces;

namespace alms.cherry.data.process.Implementation.Repositories.ProductRepositories
{
    public class ProductRepository : CherryBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IApplicationConfigurationManager manager, ICache cache, IUnitOfWork unitOfWork) : base(manager, cache, unitOfWork)
        {
        }

        public async Task<TResult> Add<TResult>(Product entity)
        {
            return await InsertAsync<TResult>(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> AddAll<TResult>(IEnumerable<Product> entities)
        {
            return await InsertAllAsync(entities, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> Delete(object id)
        {
            return await DeleteAsync(id, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> Delete(Product entity)
        {
            return await DeleteAsync(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<TResult> Merge<TResult>(Product entity)
        {
            return await MergeAsync<TResult>(entity, transaction: UnitOfWork.Transaction);
        }

        public Task<Product> Query(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> Query(Guid id)
        {
            var query = await QueryAsync(TableNames.ProductTable, e => e.Id == id, cacheKey: nameof(Product));
            return query.FirstOrDefault();
        }

        public async Task<int> Update(Product entity)
        {
            return await UpdateAsync(entity, entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await QueryAllAsync(TableNames.ProductTable, cacheKey: nameof(Product));
        }
    }
}
