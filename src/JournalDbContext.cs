using Microsoft.EntityFrameworkCore;

namespace Journal
{
    public class JournalDbContext : DbContext // database
    {
        public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options)
        {

        }

        public DbSet<Journeys.Table> Journeys { get; set; } // table 
        public DbSet<Notes.Table> Notes { get; set; }
        public DbSet<Users.Table> Users { get; set; }
        public DbSet<MTM.JourneyUsers.Table> JourneyUsers { get; set; }
    }
}
