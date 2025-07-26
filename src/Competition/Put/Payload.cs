using System.ComponentModel.DataAnnotations;

namespace Journal.Competition.Put;

public class Payload
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string Location { get; set; } = string.Empty;
    
    [Required]
    public Guid ExerciseId { get; set; }
    
    [Required]
    public DateTime DateTime { get; set; }
    
    [Required]
    public required Type Type { get; set; }
}
