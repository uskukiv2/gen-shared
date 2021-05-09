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
    public class EmployeeRepository : BaseRepository<Employee, NpgsqlConnection>, IEmployeeRepository
    {
        private IUnitOfWork _unitOfWork;

        public EmployeeRepository(string connectionString) : base(connectionString)
        {
        }

        public EmployeeRepository(string connectionString, int? commandTimeout) : base(connectionString, commandTimeout)
        {
        }

        public EmployeeRepository(string connectionString, ICache cache) : base(connectionString, cache)
        {
        }

        public EmployeeRepository(string connectionString, ITrace trace) : base(connectionString, trace)
        {
        }

        public EmployeeRepository(string connectionString, IStatementBuilder statementBuilder) : base(connectionString, statementBuilder)
        {
        }

        public EmployeeRepository(string connectionString, ConnectionPersistency connectionPersistency) : base(connectionString, connectionPersistency)
        {
        }

        public EmployeeRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration) : base(connectionString, commandTimeout, cache, cacheItemExpiration)
        {
        }

        public EmployeeRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration, ITrace trace = null) : base(connectionString, commandTimeout, cache, cacheItemExpiration, trace)
        {
        }

        public EmployeeRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration, ITrace trace = null, IStatementBuilder statementBuilder = null) : base(connectionString, commandTimeout, cache, cacheItemExpiration, trace, statementBuilder)
        {
        }

        public EmployeeRepository(string connectionString, int? commandTimeout, ConnectionPersistency connectionPersistency, ICache cache, int? cacheItemExpiration, ITrace trace = null, IStatementBuilder statementBuilder = null) : base(connectionString, commandTimeout, connectionPersistency, cache, cacheItemExpiration, trace, statementBuilder)
        {
        }

        public EmployeeRepository(IApplicationConfigurationManager manager, ICache cache, IUnitOfWork unitOfWork) : base(manager.GetDefaultConfiguration().BuildConnectionString(), cache)
        {
            Attach(unitOfWork);
        }

        public void Attach(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResult> Add<TResult>(Employee entity)
        {
            return await InsertAsync<TResult>(entity, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> AddAll<TResult>(IEnumerable<Employee> entities)
        {
            return await InsertAllAsync(entities, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> Delete(object id)
        {
            return await DeleteAsync(id, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> Delete(Employee entity)
        {
            return await DeleteAsync(entity, transaction: _unitOfWork.Transaction);
        }

        public async Task<TResult> Merge<TResult>(Employee entity)
        {
            return await MergeAsync<TResult>(entity, transaction: _unitOfWork.Transaction);
        }

        public Task<Employee> Query(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Employee> Query(Guid id)
        {
            var query = await QueryAsync(TableNames.EmployeeTable, e => e.Id == id);
            return query.FirstOrDefault();
        }

        public async Task<int> Update(Employee entity)
        {
            return await UpdateAsync(entity, entity, transaction: _unitOfWork.Transaction);
        }

        public async Task<Employee> GetEmployeWithRotationAndUsername(string username, byte[] rotation)
        {
            var queryGroup = new QueryGroup(new List<QueryField>
            {
                new QueryField(nameof(Employee.UserName), Operation.Equal, username),
                new QueryField(nameof(Employee.Rotation), Operation.Equal, rotation),
                new QueryField(nameof(Employee.Active), Operation.Equal, true)
            });
            var query = await QueryAsync(TableNames.EmployeeTable, queryGroup);

            return query.FirstOrDefault();
        }
    }
}
