namespace Journal.Databases.Campaigns.Tables.MeetUp
{
    public class Table
    {
        public Guid Id { get; set; }
        public String ParticipantIds { get; set; }

        public String Title { get; set; }
        public DateTime DateTime { get; set; }
        public String Location { get; set; }
        public String CoverImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }


    }
}
