using Microsoft.AspNetCore.Mvc;

namespace Journal.Gadgets;

public class Controller:ControllerBase
{
    private readonly ILogger<Controller> _logger;
    private readonly JournalDbContext _context;
    public Controller(ILogger<Controller> logger, JournalDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet]
    public IActionResult Get()
    {
        var gadgets = _context.Gadgets.ToList();
        return Ok(gadgets);
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
