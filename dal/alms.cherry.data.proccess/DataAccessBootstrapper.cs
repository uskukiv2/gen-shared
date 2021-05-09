using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using alms.cherry.common.Extensions;
using alms.cherry.data.objects.Mappings;
using alms.cherry.data.process.Implementation;
using alms.cherry.data.process.Implementation.Repositories.ERepositories;
using alms.cherry.data.process.Infrastructure;
using alms.cherry.data.process.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using RepoDb;
using RepoDb.Interfaces;

namespace alms.cherry.data.process
{
    public static class DataAccessBootstrapper
    {
        public static void RegisterDataAccess(IEnumerable<Type> allTypes, IServiceCollection service)
        {
            var mergedTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes()).Union(allTypes).ToList();

            PostgreSqlBootstrap.Initialize();
            service.AddSingleton<ICache, CherryCache>();
            service.AddTransient<IUnitOfWork, UnitOfWork>();
            RegisterRepositories(mergedTypes, service);
            RegisterMaps();
        }

        private static void RegisterRepositories(IEnumerable<Type> allTypes, IServiceCollection service)
        {
            var baseType = typeof(IRepository);
            var baseGenericType = typeof(IRepository<>);
            var disposableType = typeof(IDisposable);

            var repositories = allTypes
                .DescendantOf(baseType)
                .OnlyInstantiableClasses()
                .Where(x => !x.IsAbstract && !x.IsGenericType);

            foreach (var repository in repositories)
            {
                var interfaceType = repository
                    .GetInterfaces()
                    .FirstOrDefault(ty => ty.IsInterface && ty != baseType && ty != baseGenericType && ty != disposableType);

                service.AddTransient(interfaceType, repository);
            }

        }

        private static void RegisterMaps()
        {
            var employeeMap = new EmployeeMap();
            var employeeProfileMap = new EmployeeProfileMap();
            var settingsMap = new EmployeeSystemSettingsMap();
        }
    }
}
