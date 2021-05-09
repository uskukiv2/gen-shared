using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace alms.cherry.data.objects.DatabaseEntities.Base
{
    public abstract class DbEntity
    {
        public abstract Type Type { get; }

        public abstract Dictionary<string, PropertyInfo> GetEntityProperties();

        public abstract MemberInfo GetEntityId();

        public abstract string GetTableName();
    }
}
