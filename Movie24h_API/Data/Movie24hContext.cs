using Microsoft.EntityFrameworkCore;
using Movie24h_API.Models;

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
        public DbSet<User> Users { get; set; }

        // Cach tao khoa ngoai trong DBContext
        //protected override void OnModelCreating(ModelBuilder modelBuilder) {
        //    modelBuilder.Entity<Movie>()
        //        .HasOne(m => m.User)  // Một bộ phim thuộc về một User
        //        .WithMany()            // Một User có thể có nhiều phim
        //        .HasForeignKey(m => m.UserId) // Khóa ngoại
        //        .OnDelete(DeleteBehavior.Cascade); // Xóa User sẽ xóa luôn Movie
        //}
    }
}
