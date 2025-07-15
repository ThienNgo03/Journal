using Journal.Databases.Campaigns.Tables.Gadget;

namespace Journal.Gadgets.Get;

public class Parameters
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
    public Guid? GadgetId { get; set; } 
    public string? Name { get; set; } 
    public string? Brand { get; set; } 
    public string? Description { get; set; } 
}
public class PageProperty
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public IEnumerable<Table> Gadgets { get; set; }= [];
}