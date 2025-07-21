using System.ComponentModel.DataAnnotations;

namespace Journal.Exercises.Update
{
    public class Payload
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

    }
}
