using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace alms.cherry.common.Managers
{
    public static class CherryResourceHelper
    {
        public static string GetStringFromLocalizationResourceFile(string level, string key)
        {
            var resource = new ResourceManager($"alms.cherry.common.ApplicationLocalization_{level}", Assembly.GetAssembly(typeof(CherryResourceHelper)));

            return resource.GetString(key);
        }
    }
}
