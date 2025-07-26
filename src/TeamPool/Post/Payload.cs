using System.ComponentModel.DataAnnotations;

namespace Journal.TeamPool.Post;

public class Payload
{
    [Required]
    public int Position { get; set; }
    [Required]
    public Guid ParticipantId { get; set; }
    [Required]
    public Guid CompetitionId { get; set; }
}
