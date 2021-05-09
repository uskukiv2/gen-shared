using alms.cherry.infra.Extensions;
using alms.cherry.infra.infrastructure;
using Npgsql;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Interfaces;

namespace alms.cherry.data.process.Infrastructure
{
    public abstract class CherryBaseRepository<TEntity> : BaseRepository<TEntity, NpgsqlConnection> where TEntity : class
    {
        protected CherryBaseRepository(string connectionString) : base(connectionString)
        {
        }

        protected CherryBaseRepository(string connectionString, int? commandTimeout) : base(connectionString, commandTimeout)
        {
        }

        protected CherryBaseRepository(string connectionString, ICache cache) : base(connectionString, cache)
        {
        }

        protected CherryBaseRepository(string connectionString, ITrace trace) : base(connectionString, trace)
        {
        }

        protected CherryBaseRepository(string connectionString, IStatementBuilder statementBuilder) : base(connectionString, statementBuilder)
        {
        }

        protected CherryBaseRepository(string connectionString, ConnectionPersistency connectionPersistency) : base(connectionString, connectionPersistency)
        {
        }

        protected CherryBaseRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration) : base(connectionString, commandTimeout, cache, cacheItemExpiration)
        {
        }

        protected CherryBaseRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration, ITrace trace = null) : base(connectionString, commandTimeout, cache, cacheItemExpiration, trace)
        {
        }

        protected CherryBaseRepository(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration, ITrace trace = null, IStatementBuilder statementBuilder = null) : base(connectionString, commandTimeout, cache, cacheItemExpiration, trace, statementBuilder)
        {
        }

        protected CherryBaseRepository(string connectionString, int? commandTimeout, ConnectionPersistency connectionPersistency, ICache cache, int? cacheItemExpiration, ITrace trace = null, IStatementBuilder statementBuilder = null) : base(connectionString, commandTimeout, connectionPersistency, cache, cacheItemExpiration, trace, statementBuilder)
        {
        }

        protected CherryBaseRepository(IApplicationConfigurationManager manager, ICache cache, IUnitOfWork unitOfWork) : base(manager.GetDefaultConfiguration().BuildConnectionString(), cache)
        {
            Attach(unitOfWork);
        }

        public void Attach(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; private set; }
    }
}