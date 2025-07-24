namespace Journal.MeetUps.Update
{
    public class Payload
    {
        public Guid Id { get; set; }
        public String ParticipantIds { get; set; }
        public String Title { get; set; }
        public DateTime DateTime { get; set; }
        public String Location { get; set; }
        public String CoverImage { get; set; }
    }
}
