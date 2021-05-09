using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alms.cherry.data.objects;
using alms.cherry.data.objects.Entities;
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
    public class EmployeeSystemSettingRepository : BaseRepository<EmployeeSystemSetting, NpgsqlConnection>, IEmployeeSystemSettingRepository
    {
        private IUnitOfWork _unitOfWork;

        public EmployeeSystemSettingRepository(IApplicationConfigurationManager manager, ICache cache, IUnitOfWork unitOfWork) : base(manager.GetDefaultConfiguration().BuildConnectionString(), cache)
        {
            Attach(unitOfWork);
        }

        public EmployeeSystemSettingRepository(string connectionString) : base(connectionString)
        {
        }

        public EmployeeSystemSettingRepository(string connectionString, int? commandTimeout) : base(connectionString, commandTimeout)
        {
        }

        public EmployeeSystemSettingRepository(string connectionString, ICache cache) : base(connectionString, cache)
        {
        }

        public EmployeeSystemSettingRepository(string connectionString, ITrace trace) : base(connectionString, trace)
        {
        }

        public EmployeeSystemSettingRepository(string connectionString, IStatementBuilder statementBuilder) : base(connectionString, statementBuilder)
        {
        }

        public EmployeeSystemSettingRepository(string connectionString, ConnectionPersistency connectionPersistency) : base(connectionString, connectionPersistency)
        {
        }

        public EmployeeSystemSettingRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration) : base(connectionString, commandTimeout, cache, cacheItemExpiration)
        {
        }

        public EmployeeSystemSettingRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration, ITrace trace = null) : base(connectionString, commandTimeout, cache, cacheItemExpiration, trace)
        {
        }

        public EmployeeSystemSettingRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration, ITrace trace = null, IStatementBuilder statementBuilder = null) : base(connectionString, commandTimeout, cache, cacheItemExpiration, trace, statementBuilder)
        {
        }

        public EmployeeSystemSettingRepository(string connectionString, int? commandTimeout, ConnectionPersistency connectionPersistency, ICache cache, int? cacheItemExpiration, ITrace trace = null, IStatementBuilder statementBuilder = null) : base(connectionString, commandTimeout, connectionPersistency, cache, cacheItemExpiration, trace, statementBuilder)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Attach(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResult> Add<TResult>(EmployeeSystemSetting entity)
        {
            return await InsertAsync<TResult>(entity, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> AddAll<TResult>(IEnumerable<EmployeeSystemSetting> entities)
        {
            return await InsertAllAsync(entities, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> Delete(object id)
        {
            return await DeleteAsync(id, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> Delete(EmployeeSystemSetting entity)
        {
            return await DeleteAsync(entity, transaction: _unitOfWork.Transaction);
        }

        public async Task<TResult> Merge<TResult>(EmployeeSystemSetting entity)
        {
            return await MergeAsync<TResult>(entity, transaction: _unitOfWork.Transaction);
        }

        public Task<EmployeeSystemSetting> Query(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeSystemSetting> Query(Guid id)
        {
            var query = await QueryAsync(TableNames.EmployeeSystemSettingTable, e => e.Id == id);
            return query.FirstOrDefault();
        }

        public async Task<int> Update(EmployeeSystemSetting entity)
        {
            return await UpdateAsync(entity, entity, transaction: _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<EmployeeSystemSetting>> GetSettingsByEmployeeId(Guid employeeId)
        {
            var queryGroup = new QueryGroup(new List<QueryField>
            {
                new QueryField(nameof(EmployeeSystemSetting.EmployeeId), Operation.Equal, employeeId)
            });
            var query = await QueryAsync(TableNames.EmployeeSystemSettingTable, queryGroup);

            return query;
        }
    }
}
