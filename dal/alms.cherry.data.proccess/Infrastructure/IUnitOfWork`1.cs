using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Npgsql;

namespace alms.cherry.data.process.Infrastructure
{
    public interface IUnitOfWork
    {
        NpgsqlConnection Connection { get; }
        DbTransaction Transaction { get; }
        void Begin();
        void Rollback();
        void Commit();
    }
}
