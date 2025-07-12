using Microsoft.EntityFrameworkCore;

namespace Journal
{
    public class JournalDbContext: DbContext // database
    {
        public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options)
        {

        }

        public DbSet<Journey> Journeys { get; set; } // table 
        //protected override void OnModelCreating(ModelBuilder modelBuilder) // phần cấu hình này có đúng không? và lúc check trên Database và So sánh thì ổn, nhưng hiện trên body có cả giờ nữa
        //{
        //    modelBuilder.Entity<Journey>()
        //        .Property(j => j.Date)
        //        .HasColumnType("date"); // 👈 Cấu hình kiểu SQL là "date"
        //}
        public DbSet<Gadgets.Table> Gadgets { get; set; }
    }
}
