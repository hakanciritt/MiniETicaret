using Microsoft.Extensions.Configuration;

namespace ETicaret.Persistence
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager manager = new();
                manager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETicaret.API"));
                manager.AddJsonFile("appsettings.json");
                return manager.GetConnectionString("SqlServer");
            }
        }
    }
}
