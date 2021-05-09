using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alms.cherry.data.objects;
using alms.cherry.data.objects.Entities.Product;
using alms.cherry.data.process.Infrastructure;
using alms.cherry.data.process.Interfaces;
using alms.cherry.infra.infrastructure;
using RepoDb.Enumerations;
using RepoDb.Interfaces;

namespace alms.cherry.data.process.Implementation.Repositories.ProductRepositories
{
    public class ProductTypeRepository : CherryBaseRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(IApplicationConfigurationManager manager, ICache cache, IUnitOfWork unitOfWork) : base(manager, cache, unitOfWork)
        {
        }

        public async Task<TResult> Add<TResult>(ProductType entity)
        {
            return await InsertAsync<TResult>(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> AddAll<TResult>(IEnumerable<ProductType> entities)
        {
            return await InsertAllAsync(entities, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> Delete(object id)
        {
            return await DeleteAsync(id, transaction: UnitOfWork.Transaction);
        }

        public async Task<int> Delete(ProductType entity)
        {
            return await DeleteAsync(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<TResult> Merge<TResult>(ProductType entity)
        {
            return await MergeAsync<TResult>(entity, transaction: UnitOfWork.Transaction);
        }

        public async Task<ProductType> Query(int id)
        {
            var query = await QueryAsync(TableNames.ProductTypeTable, e => e.Id == id, cacheKey: nameof(ProductType));
            return query.FirstOrDefault();
        }

        public async Task<ProductType> Query(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(ProductType entity)
        {
            return await UpdateAsync(entity, entity, transaction: UnitOfWork.Transaction);
        }
    }
}