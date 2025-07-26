using System.ComponentModel.DataAnnotations;

namespace Journal.TeamPool.Put;

public class Payload
{
    [Required]
    public Guid Id { get; set; }
    public int Position { get; set; }
    [Required]
    public Guid CompetitionId { get; set; }
}
