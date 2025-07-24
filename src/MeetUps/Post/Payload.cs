using System.ComponentModel.DataAnnotations;

namespace Journal.MeetUps.Post
{
    public class Payload
    {
        [Required]
        public String ParticipantIds { get; set; }
        [Required]
        public String Title { get; set; } = string.Empty;
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public String Location { get; set; } = string.Empty;
        [Required]
        public String CoverImage { get; set; } = string.Empty;
    }
}
