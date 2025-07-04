using System.Configuration;

namespace AvtoServis.Data.Configuration
{
    public static class DatabaseConfig
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["AutoServiceDBConnection"].ConnectionString;
    }
}
