using System.ComponentModel.DataAnnotations;

namespace Journal.SoloPool.Post;

public class Payload
{
    [Required]
    public Guid WinnerId { get; set; }
    [Required]
    public Guid LoserId { get; set; }
    [Required]
    public Guid CompetitionId { get; set; }
}
