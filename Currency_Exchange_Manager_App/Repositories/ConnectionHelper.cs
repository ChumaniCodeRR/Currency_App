using NuGet.Protocol;
using StackExchange.Redis;
using System;

namespace Currency_Exchange_Manager_App.Repositories
{
    public class ConnectionHelper
    {

        static ConnectionHelper()
        {
            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                {
                    return ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisURL"]);

                });
        }
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
