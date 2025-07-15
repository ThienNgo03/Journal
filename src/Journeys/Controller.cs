using Journal.Databases.Campaigns;
using Journal.Journeys.Delete;
using Journal.Journeys.Post;
using Journal.Journeys.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wolverine;
using Wolverine.Runtime;


namespace Journal.Journeys
{
    [ApiController]
    [Route("Journeys")]
    public class Controller : ControllerBase
    {
        private readonly IMessageBus _messageBus;
        private readonly ILogger<Controller> _logger;

        private readonly JournalDbContext _context; //biến đại diện cho database

        public Controller(ILogger<Controller> logger, JournalDbContext context, IMessageBus messageBus)
        {
            _logger = logger;
            _context = context; // gán database vào biến(_context) đã tạo
            _messageBus = messageBus;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Get.Parameters parameters)// phải có dấu ? sau mỗi property, cho phép để trống chúng khi Get, nếu không sẽ lỗi
        {
            var journeys = _context.Journeys.AsQueryable(); //lấy table ra, nhưng chưa đâm xuống Database
            //Query ID
            if (parameters.id.HasValue)
                journeys = journeys.Where(x => x.Id == parameters.id);
            //Query Content
            if (!string.IsNullOrEmpty(parameters.content))
                journeys = journeys.Where(x => x.Content.Contains(parameters.content));
            //Query Location
            if (!string.IsNullOrEmpty(parameters.location))
                journeys = journeys.Where(x => x.Location.Contains(parameters.location));
            //Query Date
            if (parameters.date.HasValue)
                journeys = journeys.Where(x => x.Date == parameters.date);
            //chia trang
            if (parameters.pageSize.HasValue && parameters.pageIndex.HasValue && parameters.pageSize > 0 && parameters.pageIndex >= 0)
                journeys = journeys.Skip(parameters.pageIndex.Value * parameters.pageSize.Value).Take(parameters.pageSize.Value);

            var result = await journeys.AsNoTracking().ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post.Payload payload)
        {
            var journey = new Journeys.Table //tạo một hàng dữ liệu mới
            {
                Id = Guid.NewGuid(),
                Content = payload.Content,
                Location = payload.Location,
                Date = payload.Date
            };
            _context.Journeys.Add(journey); // add hàng dữ liệu mới vào table
            await _context.SaveChangesAsync(); // lưu lại table
            return CreatedAtAction(nameof(Get), journey.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Delete.Parameters parameters) // bắt buộc phải có id để tìm nên không cần dấu ?
        {
            var journey = await _context.Journeys.FindAsync(parameters.Id);// chờ để ASP.NET tìm và lấy ra data với id được cho, hàm FindAsync chỉ để tìm khóa chính của bảng
            if (journey == null) //nếu data vẫn là null sau khi tìm nghĩa là không có data với id được cho
            {
                return NotFound();
            }
            _context.Journeys.Remove(journey); //xóa data tìm được khỏi table hiện tại
            await _context.SaveChangesAsync();
            await _messageBus.PublishAsync(new Delete.Messager.Message(parameters.Id, parameters.DeleteNotes)); // bắn qua handler
            return NoContent(); //201
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Update.Payload payload)
        {
            var journey = await _context.Journeys.FindAsync(payload.Id);
            if (journey == null)
            {
                return NotFound();
            }
            journey.Content = payload.Content; // cập nhật data cũ với data mà người dùng nhập vào
            journey.Date = payload.Date;
            journey.Location = payload.Location;
            _context.Journeys.Update(journey);
            await _context.SaveChangesAsync();
            return NoContent(); //201
        }
    }
}
