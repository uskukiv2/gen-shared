using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alms.cherry.data.objects;
using alms.cherry.data.objects.Entities.Product;
using alms.cherry.data.process.Infrastructure;
using alms.cherry.data.process.Interfaces;
using alms.cherry.infra.infrastructure;
using RepoDb.Interfaces;

namespace alms.cherry.data.process.Implementation.Repositories.ProductRepositories
{
    public class ProductCategoryRepository : CherryBaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IApplicationConfigurationManager manager, ICache cache, IUnitOfWork unitOfWork) : base(manager, cache, unitOfWork)
        {
        }

        public async Task<TResult> Add<TResult>(ProductCategory entity)
        {
            return await InsertAsync<TResult>(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> AddAll<TResult>(IEnumerable<ProductCategory> entities)
        {
            return await InsertAllAsync(entities, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> Delete(object id)
        {
            return await DeleteAsync(id, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> Delete(ProductCategory entity)
        {
            return await DeleteAsync(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<TResult> Merge<TResult>(ProductCategory entity)
        {
            return await MergeAsync<TResult>(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<ProductCategory> Query(int id)
        {
            var query = await QueryAsync(TableNames.ProductCategoryTable, e => e.Id == id, cacheKey: nameof(ProductCategory));
            return query.FirstOrDefault();
        }

        public Task<ProductCategory> Query(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(ProductCategory entity)
        {
            return await UpdateAsync(entity, entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await QueryAllAsync(TableNames.ProductCategoryTable);
        }
    }
}