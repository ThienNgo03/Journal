namespace Journal.Databases.Campaigns.Tables.TeamPool;

public class Table
{
    public Guid Id { get; set; }
    public Guid WinnerId { get; set; }
    public Guid CompetitionId { get; set; }
    public DateTime CreatedDate { get; set; }
}
