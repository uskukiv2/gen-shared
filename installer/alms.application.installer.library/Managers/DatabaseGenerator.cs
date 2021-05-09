using System;
using System.Collections.Generic;
using alms.application.installer.library.Database;
using alms.cherry.common;
using alms.cherry.data.objects;
using alms.cherry.data.objects.DatabaseEntities.Base;
using alms.cherry.data.objects.Entities.Employees;
using alms.cherry.data.process;
using Npgsql;
using NpgsqlTypes;
using RepoDb;
using Serilog;

namespace alms.application.installer.library.Managers
{
    public class DatabaseGenerator
    {
        public DatabaseGenerator()
        {
            PostgreSqlBootstrap.Initialize();
        }

        public void CreateDatabaseAndUser(Tuple<string, string, string> tuple)
        {
            Console.WriteLine("===Creating user if not exists===");
            var user = GenerateUserIfNotExists(tuple.Item1, "postgres", "root", tuple.Item3, tuple.Item2);

            Console.WriteLine("===Removing old database if exists and creating new===");
            var database = GenerateDatabase(tuple.Item1, "postgres", "root", tuple.Item2);

            Console.WriteLine("===Start generate tables===");
            GenerateTables(tuple.Item1, user, tuple.Item3, database);

            Console.WriteLine("===Add root user into system===");
            AddSystemUserIntoCherry(tuple.Item1, user, tuple.Item3, database);
        }

        public void UpdateDatabaseIfNeeded(Tuple<string, string, string> tuple)
        {
            Console.WriteLine("===Start update database if needed===");
            GenerateTables(tuple.Item1, $"login_{tuple.Item2}_cherry", tuple.Item3, $"{tuple.Item2}_cherry");
        }

        private static void GenerateTables(string server, string user, string password, string database)
        {
            try
            {
                var manager = new MigrationManger();
                var credentials = GetServerCredentials(server, user, password, database);
                manager.Init(credentials);
                manager.StartUpdateDatabase();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, e.Message);
                Console.WriteLine(e);
            }
        }

        private static void AddSystemUserIntoCherry(string server, string user, string password, string database)
        {
            var userManager = new UserManager();
            var employee = userManager.GenerateEmployee(password);
            var employeProfile = userManager.GenerateEmployeeProfile(employee, "Cherry", "System");
            using var connection = new NpgsqlConnection(GetServerCredentials(server, user, password, database));
            
            connection.Open(); 
            
            connection.Insert(TableNames.EmployeeTable, employee);
            
            employee.EmployeeProfile = employeProfile;
            connection.Insert(TableNames.EmployeeProfileTable, employee.EmployeeProfile);
            
            employee.EmployeeProfileId = employeProfile.Id;
            connection.Update(TableNames.EmployeeTable, employee);

            connection.Close();
        }

        private static string GenerateUserIfNotExists(string server, string user, string adminPassword, string userPassword, string customerName)
        {
            try
            {
                using var connection = new NpgsqlConnection(GetServerCredentials(server, user, adminPassword));
                connection.Open();

                using (var dropLogin = new NpgsqlCommand($@"DROP ROLE IF EXISTS login_{customerName}_cherry; COMMIT;", connection))
                {
                    dropLogin.ExecuteNonQuery();
                }

                var script1 = $@"CREATE ROLE login_{customerName}_cherry WITH
                                            LOGIN
                                            NOSUPERUSER
                                            INHERIT
                                            CREATEDB
                                            NOCREATEROLE
                                            NOREPLICATION
                                            ENCRYPTED PASSWORD '{userPassword}'; COMMIT;";
                using (var npgsqlCommand = new NpgsqlCommand(script1, connection))
                {
                    npgsqlCommand.ExecuteNonQuery();
                }

                connection.Close();
                return $"login_{customerName}_cherry";
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, e.Message);
                Console.WriteLine(e);
                return string.Empty;
            }
        }

        private static string GenerateDatabase(string server, string user, string password, string customerName)
        {
            try
            {
                using var connection = new NpgsqlConnection(GetServerCredentials(server, user, password));
                connection.Open();
                using (var databaseDeletion = new NpgsqlCommand($@"DROP DATABASE IF EXISTS {customerName}_cherry; COMMIT;", connection))
                {
                    databaseDeletion.ExecuteScalar();
                }

                var script1 = $@"CREATE DATABASE {customerName}_cherry
                                                            WITH 
                                                            OWNER = {user}
                                                            ENCODING = 'UTF8'
                                                            LC_COLLATE = 'en_US.utf8'
                                                            LC_CTYPE = 'en_US.utf8'
                                                            TABLESPACE = pg_default
                                                            CONNECTION LIMIT = 35
                                                            TEMPLATE template0; COMMIT;";
                using (var databaseCreation = new NpgsqlCommand(script1, connection))
                {
                    databaseCreation.ExecuteNonQuery();
                }

                connection.Close();
                return $"{customerName}_cherry";
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, e.Message);
                Console.WriteLine(e);
                return string.Empty;
            }
        }

        private static string GetServerCredentials(string host, string hostUser, string hostUserPassword)
        {
            var stringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Username = hostUser,
                Password = hostUserPassword,
                Database = "postgres",
                Port = 5432
            };
            return stringBuilder.ToString();
        }
        private static string GetServerCredentials(string host, string hostUser, string hostUserPassword, string database)
        {
            var stringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Username = hostUser,
                Password = hostUserPassword,
                Database = database,
                Port = 5432
            };
            return stringBuilder.ToString();
        }

        private static IEnumerable<DbEntity> GetTables() => new DbEntities().GetEntities();
    }
}
