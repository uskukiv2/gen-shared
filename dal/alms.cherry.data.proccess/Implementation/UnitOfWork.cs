using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using alms.cherry.common.Models;
using alms.cherry.data.process.Infrastructure;
using alms.cherry.infra.Extensions;
using alms.cherry.infra.infrastructure;
using Npgsql;

namespace alms.cherry.data.process.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Configuration _configuration;


        public UnitOfWork(IApplicationConfigurationManager manager)
        {
            _configuration = manager.GetActiveApplicationConfiguration().ConfigurationList.FirstOrDefault(x => x.Default);
        }

        public NpgsqlConnection Connection { get; private set; }

        public DbTransaction Transaction { get; private set; }

        public void Begin()
        {
            if (Transaction != null)
            {
                throw new InvalidOperationException("Cannot start a new transaction while the existing other one is still open.");
            }
            var connection = EnsureConnection();
            Transaction = connection.BeginTransaction();
        }

        public void Rollback()
        {
            if (Transaction == null)
            {
                throw new InvalidOperationException("There is no active transaction to rollback.");
            }
            using (Transaction)
            {
                Transaction.Rollback();
            }
            Transaction = null;
        }

        public void Commit()
        {
            if (Transaction == null)
            {
                throw new InvalidOperationException("There is no active transaction to commit.");
            }
            using (Transaction)
            {
                Transaction.Commit();
            }
            Transaction = null;
        }

        private NpgsqlConnection EnsureConnection()
        {
            return Connection ??= new NpgsqlConnection(_configuration.BuildConnectionString());
        }
    }
}
