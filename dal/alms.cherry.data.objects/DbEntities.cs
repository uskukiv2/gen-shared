using System;
using System.Collections.Generic;
using System.Text;
using alms.cherry.data.objects.DatabaseEntities;
using alms.cherry.data.objects.DatabaseEntities.Base;

namespace alms.cherry.data.objects
{
    public class DbEntities
    {
        private readonly IEnumerable<DbEntity> _entities;

        public DbEntities()
        {
            _entities = LoadEntities();
        }

        public IEnumerable<DbEntity> GetEntities() => _entities;

        private IEnumerable<DbEntity> LoadEntities()
        {
            return new List<DbEntity>
            {
                new EmployeeEntity(TableNames.EmployeeTable)
            };
        }
    }
}
