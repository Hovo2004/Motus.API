using Microsoft.EntityFrameworkCore;


namespace Motus.API.Data.DAO {
    public class MainDbContext : DbContext {
        public MainDbContext(DbContextOptions options) : base(options) { }

        // Declaring the tables of database
        //public DbSet<RandomEntity> Randoms { get; set; }
    }
}
