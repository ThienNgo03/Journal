namespace Journal.MeetUps.Get
{
    public class Parameters
    {

        public Guid? Id { get; set; }
        public String? ParticipantIds { get; set; }
        public String? Title { get; set; }
        public DateTime? DateTime { get; set; }
        public String? Location { get; set; }
        public String? CoverImage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

    }
}
