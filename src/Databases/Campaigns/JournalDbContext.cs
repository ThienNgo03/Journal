using Journal.Databases.Campaigns.Tables.Gadget;
using Journal.Databases.Campaigns.Tables.Journey;
using Journal.Databases.Campaigns.Tables.Note;
using Journal.Databases.Campaigns.Tables.User;
using Microsoft.EntityFrameworkCore;

namespace Journal.Databases.Campaigns
{
    public class JournalDbContext : DbContext // database
    {
        public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options)
        {

        }

        public DbSet<Tables.Journey.Table> Journeys { get; set; } // table 
        public DbSet<Tables.Note.Table> Notes { get; set; }
        public DbSet<Tables.User.Table> Users { get; set; }
        public DbSet<Tables.JourneyUsers.Table> JourneyUsers { get; set; }
        public DbSet<Tables.Gadget.Table> Gadgets { get; set; }
        public DbSet<Tables.JourneyGadgets.Table> JourneyGadgets { get; set; }
    }
}
