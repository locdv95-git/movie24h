using Microsoft.EntityFrameworkCore;
using Movie24h_API.Model;

namespace Movie24h_API.Data {
    public class Movie24hContext : DbContext {
        protected readonly IConfiguration Configuration;
        public Movie24hContext(IConfiguration configuration) {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Member> Members { get; set; }
    }
}
