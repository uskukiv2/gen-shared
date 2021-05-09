using System;
using System.Linq;
using System.Threading.Tasks;
using alms.application.installer.library.Migrations;
using alms.cherry.common.Extensions;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace alms.application.installer.library.Database
{
    public class MigrationManger
    {
        private string _connectionString;
        private IServiceProvider _provider;


        public void Init(string connectionString)
        {
            _connectionString = connectionString;
            _provider = CreateServices();
        }

        public void StartUpdateDatabase()
        {
            using var scope = _provider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
        }

        private void UpdateDatabase(IServiceProvider scopeServiceProvider)
        {
            // Instantiate the runner
            var runner = scopeServiceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }

        private IServiceProvider CreateServices()
        {
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes()).ToArray()
                .DescendantOf(typeof(Migration))
                .OnlyInstantiableClasses().Select(x => x.Assembly).ToArray();

            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(_connectionString)
                    .ScanIn(types).For.Migrations())
                    .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider();
        }
    }
}
