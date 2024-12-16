using Microsoft.EntityFrameworkCore;
using Motus.API.Data.Entities;


namespace Motus.API.Data.DAO {
    public class MainDbContext : DbContext {
        public MainDbContext(DbContextOptions options) : base(options) { }

        // Declaring the tables of database
        public DbSet<UserEntity> Users { get; set; }
    }
}
