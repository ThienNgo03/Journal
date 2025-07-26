using System.ComponentModel.DataAnnotations;

namespace Journal.TeamPool.Post;

public class Payload
{
    [Required]
    public Guid WinnerId { get; set; }
    [Required]
    public Guid CompetitionId { get; set; }
}
