using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace alms.cherry.common.Models
{
    public class ApplicationConfiguration
    {
        public ApplicationConfiguration()
        {
            ConfigurationList = new List<Configuration>();
            LoggedInUsers = new List<UserLoginModel>();
        }

        public ICollection<Configuration> ConfigurationList { get; set; }

        public IEnumerable<UserLoginModel> LoggedInUsers { get; set; }
    }

    public class Configuration
    {
        [JsonIgnore]
        public int ConfigurationId { get; set; }

        public string ConfigurationName { get; set; }

        public MainConnection MainConnection { get; set; }

        public bool Default { get; set; }
    }

    public class MainConnection
    {
        public string Host { get; set; }

        public string DatabaseName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int CacheItemExpiration { get; set; }

        [JsonIgnore]
        public SecureString SecureRotation { get; set; }
    }
}
