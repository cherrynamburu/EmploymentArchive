using System;
using System.Configuration;
namespace EmploymentArchive
{
    internal static class Config
    {
        internal static string ConnectionString;

        static Config()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["SqlServerConnectionString"].ConnectionString;
        }
    }
}
