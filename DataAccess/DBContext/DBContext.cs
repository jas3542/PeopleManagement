using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PeopleManagement
{
    public class DBContext : DbContext
    {
        public DbSet<Person> Person { get; set; }

        private IConfiguration _config;
        public DBContext(IConfiguration config)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Reading the connection string:
            var connectionString = _config["DBConnection:url"];
            optionsBuilder.UseSqlServer(connectionString);

        }
    }
}
