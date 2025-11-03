using Microsoft.EntityFrameworkCore;
namespace KAShope.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=db31387.public.databaseasp.net; Database=db31387; User Id=db31387; Password=F-q4bN3%!5pM; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.Category>().HasData(
                new Models.Category { Id = 1, Name = "Mobile" },
                new Models.Category { Id = 2, Name = "Tablets" },
                new Models.Category { Id = 3, Name = "Laptops" }
                );
        }
    }
}
