using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gen.common.Extensions
{
    public static class TypeEnumerableExtensions
    {
        public static IEnumerable<Type> DescendantOf(this IEnumerable<Type> source, Type parentType)
            => source.Where(x => x != parentType && parentType.IsAssignableFrom(x));

        public static IEnumerable<Type> OnlyInstantiableClasses(this IEnumerable<Type> source)
            => source.Where(x => x.IsClass && !x.IsAbstract);

        public static async Task<List<TSource>> ToListAsync<TSource>(this IEnumerable<TSource> enumerable)
        {
            var task = Task.Factory.StartNew(async () => enumerable.ToList());
            return await await task;
        }
    }
}
