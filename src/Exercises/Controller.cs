﻿namespace Journal.Exercises
{
    [ApiController]
    [Route("Exercises")]
    public class Controller : ControllerBase
    {
        private readonly IMessageBus _messageBus;
        private readonly JournalDbContext _context;
        private readonly ILogger<Controller> _logger;

        public Controller(IMessageBus messageBus, JournalDbContext context, ILogger<Controller> logger)
        {
            _context = context;
            _logger = logger;
            _messageBus = messageBus;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Get.Parameters parameters)
        {
            var query = _context.Exercises.AsQueryable();

            if (parameters.Id.HasValue)
                query = query.Where(x => x.Id == parameters.Id);
            if (!string.IsNullOrEmpty(parameters.Name))
                query = query.Where(x => x.Name.Contains(parameters.Name));
            if (!string.IsNullOrEmpty(parameters.Description))
                query = query.Where(x => x.Description.Contains(parameters.Description));
            if (parameters.CreatedDate.HasValue)
                query = query.Where(x => x.CreatedDate == parameters.CreatedDate);
            if(parameters.LastUpdated.HasValue)
                query = query.Where(x => x.LastUpdated == parameters.LastUpdated);

            if (parameters.PageSize.HasValue && parameters.PageIndex.HasValue && parameters.PageSize > 0 && parameters.PageIndex >= 0)
                query = query.Skip(parameters.PageIndex.Value * parameters.PageSize.Value).Take(parameters.PageSize.Value);

            var result = await query.AsNoTracking().ToListAsync();
            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromBody] Post.Payload payload)
        {
            var exercise = new Databases.Journal.Tables.Excercise.Table
            {
                Id = Guid.NewGuid(),
                Name = payload.Name,
                Description = payload.Description,
                CreatedDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            await _messageBus.PublishAsync(new Post.Messager.Message(exercise.Id));
            return CreatedAtAction(nameof(Get), exercise.Id);
        }

        [HttpPut]

        public async Task<IActionResult> Put([FromBody] Update.Payload payload)
        {
            var exercise = await _context.Exercises.FindAsync(payload.Id);
            if(exercise == null)
            {
                return NotFound();
            }

            exercise.Name = payload.Name;
            exercise.Description = payload.Description;
            exercise.LastUpdated = DateTime.UtcNow;
            _context.Exercises.Update(exercise);
            await _context.SaveChangesAsync();
            await _messageBus.PublishAsync(new Update.Messager.Message(payload.Id));
            return NoContent();
        }

        [HttpDelete]

        public async Task<IActionResult> Delete([FromQuery] Delete.Parameters parameters)
        {
            var exercise = await _context.Exercises.FindAsync(parameters.Id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            await _messageBus.PublishAsync(new Delete.Messager.Message(parameters.Id));
            return NoContent();
        }
    }
}
