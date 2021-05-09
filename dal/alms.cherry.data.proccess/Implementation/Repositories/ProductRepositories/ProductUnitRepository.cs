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
    public class ProductUnitRepository : CherryBaseRepository<ProductUnit>, IProductUnitRepository
    {
        public ProductUnitRepository(IApplicationConfigurationManager manager, ICache cache, IUnitOfWork unitOfWork) : base(manager, cache, unitOfWork)
        {
        }

        public async Task<TResult> Add<TResult>(ProductUnit entity)
        {
            return await InsertAsync<TResult>(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> AddAll<TResult>(IEnumerable<ProductUnit> entities)
        {
            return await InsertAllAsync(entities, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> Delete(object id)
        {
            return await DeleteAsync(id, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> Delete(ProductUnit entity)
        {
            return await DeleteAsync(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<TResult> Merge<TResult>(ProductUnit entity)
        {
            return await MergeAsync<TResult>(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<ProductUnit> Query(int id)
        {
            var query = await QueryAsync(TableNames.ProductUnitTable, e => e.Id == id, cacheKey: nameof(ProductUnit));
            return query.FirstOrDefault();
        }

        public Task<ProductUnit> Query(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(ProductUnit entity)
        {
            return await UpdateAsync(entity, entity, transaction: UnitOfWork.Transaction);
        }
    }
}