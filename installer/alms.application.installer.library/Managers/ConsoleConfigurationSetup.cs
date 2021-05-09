using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using alms.cherry.common.Models;
using alms.cherry.infra.implement;
using alms.cherry.infra.infrastructure;

namespace alms.application.installer.library.Managers
{
    public class ConsoleConfigurationSetup
    {
        private IApplicationConfigurationManager _configurationManager;

        public ConsoleConfigurationSetup()
        {
        }

        public void Init()
        {
            var file = LoadFile();

            _configurationManager = new ApplicationConfigurationManager(file);
        }

        public void CreateConfiguration(Tuple<string, string, string> tuple)
        {
            var config = InternalCreateConfiguration(tuple.Item1, $"{tuple.Item2}_cherry",$"login_{tuple.Item2}_cherry", tuple.Item3);
            _configurationManager.SaveConfiguration(config);
            Console.WriteLine("Configuration creation complete");
        }

        private string LoadFile()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), $"cherry\\configurations");
            var file = Path.Combine(path, "main-config.json");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (File.Exists(file))
            {
                File.Delete(file);
            }

            using var fs = File.Create(file);
            var dataToWrite = new UTF8Encoding(true).GetBytes("{ }");
            fs.Write(dataToWrite, 0, dataToWrite.Length);
            fs.Close();

            return file;
        }

        private ApplicationConfiguration InternalCreateConfiguration(string server, string database, string username, string password)
        {
            var configuration = new ApplicationConfiguration
            {
                ConfigurationList = new List<Configuration>
                {
                    new Configuration
                    {
                        ConfigurationId = 0,
                        ConfigurationName = "default",
                        Default = true,
                        MainConnection = new MainConnection
                        {
                            CacheItemExpiration = 1500,
                            DatabaseName = database,
                            Host = server,
                            Username = username,
                            Password = password
                        }
                    }
                },
                LoggedInUsers = new List<UserLoginModel>()
            };

            return configuration;
        }
    }
}
