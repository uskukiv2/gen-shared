using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alms.cherry.data.objects;
using alms.cherry.data.objects.Entities.Employees;
using alms.cherry.data.process.Infrastructure;
using alms.cherry.data.process.Interfaces;
using alms.cherry.infra.Extensions;
using alms.cherry.infra.infrastructure;
using Npgsql;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Interfaces;

namespace alms.cherry.data.process.Implementation.Repositories.ERepositories
{
    public class EmployeeProfileRepository : BaseRepository<EmployeeProfile, NpgsqlConnection>, IEmployeeProfileRepository
    {
        private IUnitOfWork _unitOfWork;

        public EmployeeProfileRepository(string connectionString) : base(connectionString)
        {
        }

        public EmployeeProfileRepository(string connectionString, int? commandTimeout) : base(connectionString, commandTimeout)
        {
        }

        public EmployeeProfileRepository(IApplicationConfigurationManager manager, ICache cache, IUnitOfWork unitOfWork) : base(manager.GetDefaultConfiguration().BuildConnectionString(), cache)
        {
            Attach(unitOfWork);
        }

        public EmployeeProfileRepository(string connectionString, ICache cache) : base(connectionString, cache)
        {
        }

        public EmployeeProfileRepository(string connectionString, ITrace trace) : base(connectionString, trace)
        {
        }

        public EmployeeProfileRepository(string connectionString, IStatementBuilder statementBuilder) : base(connectionString, statementBuilder)
        {
        }

        public EmployeeProfileRepository(string connectionString, ConnectionPersistency connectionPersistency) : base(connectionString, connectionPersistency)
        {
        }

        public EmployeeProfileRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration) : base(connectionString, commandTimeout, cache, cacheItemExpiration)
        {
        }

        public EmployeeProfileRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration, ITrace trace = null) : base(connectionString, commandTimeout, cache, cacheItemExpiration, trace)
        {
        }

        public EmployeeProfileRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration, ITrace trace = null, IStatementBuilder statementBuilder = null) : base(connectionString, commandTimeout, cache, cacheItemExpiration, trace, statementBuilder)
        {
        }

        public EmployeeProfileRepository(string connectionString, int? commandTimeout, ConnectionPersistency connectionPersistency, ICache cache, int? cacheItemExpiration, ITrace trace = null, IStatementBuilder statementBuilder = null) : base(connectionString, commandTimeout, connectionPersistency, cache, cacheItemExpiration, trace, statementBuilder)
        {
        }

        public void Attach(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResult> Add<TResult>(EmployeeProfile entity)
        {
            return await InsertAsync<TResult>(entity, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> AddAll<TResult>(IEnumerable<EmployeeProfile> entities)
        {
            return await InsertAllAsync(entities, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> Delete(object id)
        {
            return await DeleteAsync(id, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> Delete(EmployeeProfile entity)
        {
            return await DeleteAsync(entity, transaction: _unitOfWork.Transaction);
        }

        public async Task<TResult> Merge<TResult>(EmployeeProfile entity)
        {
            return await MergeAsync<TResult>(entity, transaction: _unitOfWork.Transaction);
        }

        public Task<EmployeeProfile> Query(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeProfile> Query(Guid id)
        {
            var query = await QueryAsync(TableNames.EmployeeProfileTable, new QueryField(nameof(EmployeeProfile.Id), Operation.Equal, id));
            return query.FirstOrDefault();
        }

        public async Task<int> Update(EmployeeProfile entity)
        {
            return await UpdateAsync(entity, entity, transaction: _unitOfWork.Transaction);
        }

        public async Task<EmployeeProfile> GetProfileFromEmployeeId(Guid employeeId)
        {
            var queryGroup = new QueryGroup(new List<QueryField>
            {
                new QueryField(nameof(EmployeeProfile.EmployeeId), Operation.Equal, employeeId),
                new QueryField(nameof(EmployeeProfile.Active), Operation.Equal, true)
            });
            var query = await QueryAsync(TableNames.EmployeeProfileTable, queryGroup);
            return query.FirstOrDefault();
        }
    }
}