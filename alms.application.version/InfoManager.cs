using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using alms.application.version.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace alms.application.version
{
    public static class InfoManager
    {
        private const string AbsolutePath = @"./Data/ApplicationSystemInfo.json";

        public static ApplicationSystemInfo GetApplicationSystemInfo()
        {
            using (var file = File.OpenText(AbsolutePath))
            {
                using (var reader = new JsonTextReader(file))
                {
                    var info = JToken.ReadFrom(reader).ToObject<ApplicationSystemInfo>();

                    return info;
                }
            }
        }
    }
}
