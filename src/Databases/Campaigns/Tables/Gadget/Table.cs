using System.ComponentModel.DataAnnotations;

namespace Journal.Databases.Campaigns.Tables.Gadget;

public class Table
{
    [Key]
    public Guid GadgetId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
