using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using alms.cherry.data.objects.Infrastructure;

namespace alms.cherry.data.objects.DatabaseEntities.Base
{
    public class DbEntity<T> : DbEntity where T : IEntity
    {
        private readonly string _entityName;

        public DbEntity(string entityName)
        {
            _entityName = entityName;
        }

        public override Type Type => typeof(T);

        public override Dictionary<string, PropertyInfo> GetEntityProperties()
        {
            var members = typeof(T).GetProperties()
                .Where(x => (x.MemberType & MemberTypes.Property) != 0 && !x.DeclaringType.IsClass).ToDictionary(x => x.Name, y => y);

            return members;
        }

        public override MemberInfo GetEntityId()
        {
            return typeof(T).GetMember("Id").FirstOrDefault();
        }

        public override string GetTableName() => _entityName;
    }
}
