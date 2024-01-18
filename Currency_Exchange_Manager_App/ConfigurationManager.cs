using Microsoft.Extensions.Configuration;
using System.IO;

namespace Currency_Exchange_Manager_App
{
    static class ConfigurationManager
    {
        public static IConfiguration AppSetting
        {
            get;
        }

        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
