using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Journal.TeamPool;

[Route("api/TeamPool")]
[ApiController]
public class Controller : ControllerBase
{
    private readonly JournalDbContext _dbContext;
    public Controller(JournalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Get.Parameters parameters)
    {
        var query = _dbContext.TeamPools.AsQueryable();
        // Filtering
        if (parameters.Id.HasValue)
        {
            query = query.Where(x => x.Id == parameters.Id.Value);
        }
        if (parameters.WinnerId.HasValue)
        {
            query = query.Where(x => x.WinnerId == parameters.WinnerId.Value);
        }
        if (parameters.CompetitionId.HasValue)
        {
            query = query.Where(x => x.CompetitionId == parameters.CompetitionId.Value);
        }
        if (parameters.CreatedDate.HasValue)
        {
            query = query.Where(x => x.CreatedDate.Date == parameters.CreatedDate.Value.Date);
        }
        // Pagination
        if (parameters.PageSize.HasValue && parameters.PageIndex.HasValue && parameters.PageSize > 0 && parameters.PageIndex >= 0)
        {
            query = query.Skip(parameters.PageIndex.Value * parameters.PageSize.Value).Take(parameters.PageSize.Value);
        }
        var result = await query.AsNoTracking().ToListAsync();
        return Ok(query);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Post.Payload payload)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var existingCompetition = await _dbContext.Competitions.FindAsync(payload.CompetitionId);
        if (existingCompetition == null)
        {
            return NotFound($"Competition with ID {payload.CompetitionId} not found.");
        }
        var teamPool = new Journal.Databases.Campaigns.Tables.TeamPool.Table
        {
            Id = Guid.NewGuid(),
            WinnerId = payload.WinnerId,
            CompetitionId = payload.CompetitionId,
            CreatedDate = DateTime.UtcNow
        };
        _dbContext.TeamPools.Add(teamPool);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = teamPool.Id });
    }
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Put.Payload payload)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var teamPool = await _dbContext.TeamPools.FindAsync(payload.Id);
        if (teamPool == null)
        {
            return NotFound();
        }
        var existingCompetition = await _dbContext.Competitions.FindAsync(payload.CompetitionId);
        if (existingCompetition == null)
        {
            return NotFound($"Competition with ID {payload.CompetitionId} not found.");
        }
        teamPool.WinnerId = payload.WinnerId;
        teamPool.CompetitionId = payload.CompetitionId;
        _dbContext.TeamPools.Update(teamPool);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromQuery] Delete.Parameters parameters)
    {
        var teamPool = await _dbContext.TeamPools.FindAsync(parameters.Id);
        if (teamPool == null)
        {
            return NotFound();
        }
        _dbContext.TeamPools.Remove(teamPool);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}
