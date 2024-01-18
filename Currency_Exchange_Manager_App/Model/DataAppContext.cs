using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.Extensions.Configuration;

namespace Currency_Exchange_Manager_App.Model
{
    public class DataAppContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DataAppContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DataAccessMySqlProvider");
        }
        public DbSet <CurrencyInfo> Currency_Info { get; set; }
        public DbSet <Convert_Currency> Convert_Currency { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySql(_connectionString, new MySqlServerVersion(new Version()));
        }
    }
}
