using Microsoft.EntityFrameworkCore;

namespace Journal
{
    public class JournalDbContext : DbContext // database
    {
        public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options)
        {

        }

        public DbSet<Journey> Journeys { get; set; } // table 
        public DbSet<Notes.Table> Notes { get; set; }
    }
}
