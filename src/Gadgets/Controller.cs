using Journal.Databases.Campaigns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Journal.Gadgets;

public class Controller:ControllerBase
{
    private readonly ILogger<Controller> _logger;
    private readonly Databases.Campaigns.JournalDbContext _context;
    public Controller(ILogger<Controller> logger, Databases.Campaigns.JournalDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromBody] Get.Parameters parameters)
    {
        var gadgets =_context.Gadgets.AsQueryable();
        if(parameters.GadgetId.HasValue)
        {
            gadgets = gadgets.Where(g => g.GadgetId == parameters.GadgetId.Value);
        }
        if(!string.IsNullOrEmpty(parameters.Name))
        {
            gadgets = gadgets.Where(g => g.Name.Contains(parameters.Name, StringComparison.OrdinalIgnoreCase));
        }
        if(!string.IsNullOrEmpty(parameters.Brand))
        {
            gadgets = gadgets.Where(g => g.Brand.Contains(parameters.Brand, StringComparison.OrdinalIgnoreCase));
        }
        if(!string.IsNullOrEmpty(parameters.Description))
        {
            gadgets = gadgets.Where(g => g.Description.Contains(parameters.Description, StringComparison.OrdinalIgnoreCase));
        }
        
        var gadgetList = await gadgets.ToListAsync();
        if (parameters.PageIndex.HasValue && parameters.PageIndex > 0 &&
            parameters.PageSize.HasValue && parameters.PageSize > 0)
        {
            gadgetList = gadgetList
                .Skip((parameters.PageIndex.Value - 1) * parameters.PageSize.Value)
                .Take(parameters.PageSize.Value)
                .ToList();
        }
        return Ok(gadgetList);
    }
    [HttpPost("gadgets")]
    public async Task<IActionResult> Create([FromBody] Post.Payload gadget)
    {
        if (gadget == null)
        {
            return BadRequest("Gadget cannot be null");
        }
        var newGadget = new Table
        {
            GadgetId = Guid.NewGuid(),
            Name = gadget.Name,
            Description = gadget.Description,
            Brand = gadget.Brand,
            CreatedDate = DateTime.UtcNow,
        };
        await _context.Gadgets.AddAsync(newGadget);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), newGadget.GadgetId);
    }
    [HttpPut("gadgets/{id}")]
    public async Task<IActionResult> Update([FromBody] Update.Payload gadget)
    {
        var existingGadget = _context.Gadgets.Find(gadget.GadgetId);
        if (existingGadget == null)
        {
            return NotFound();
        }
        if(!string.IsNullOrEmpty(gadget.Name))
            existingGadget.Name = gadget.Name;
        if(!string.IsNullOrEmpty(gadget.Description))
            existingGadget.Description = gadget.Description;
        if(!string.IsNullOrEmpty(gadget.Brand))
            existingGadget.Brand = gadget.Brand;
        existingGadget.UpdatedDate = DateTime.UtcNow;
        _context.Update(existingGadget);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("gadgets/{id}")]
    public async Task<IActionResult> Delete(Delete.Paramters paramters)
    {
        var gadget = await _context.Gadgets.FindAsync(paramters.GadgetId);
        if (gadget == null)
        {
            return NotFound();
        }
        _context.Gadgets.Remove(gadget);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
