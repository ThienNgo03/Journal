using System.ComponentModel.DataAnnotations;

namespace Journal.Exercises.Post
{
    public class Payload
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
