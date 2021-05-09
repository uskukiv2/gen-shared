using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using alms.cherry.data.objects.DatabaseEntities.Base;

namespace alms.cherry.data.objects.Extensions
{
    public static class EntityExtensions
    {
        public static string GenerateScript(this DbEntity entity)
        {
            var script = $@"CREATE TABLE IF NOT EXISTS {entity.GetTableName()} ({GenerateColumns(entity.GetEntityProperties())})";

            return string.Empty;
        }

        private static string GenerateColumns(IDictionary<string, PropertyInfo> getEntityProperties)
        {
            foreach (var entityProperty in getEntityProperties)
            {
                var columnCreation = $"{entityProperty.Key} {GetType(entityProperty.Value.PropertyType)}";
            }

            return string.Empty;
        }

        private static string GetType(Type valuePropertyType)
        {
            return string.Empty;
        }
    }
}
