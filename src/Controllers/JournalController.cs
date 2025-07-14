using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Journal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JournalController : ControllerBase
    {

        private readonly ILogger<JournalController> _logger;

        private readonly JournalDbContext _context; //biến đại diện cho database

        public JournalController(ILogger<JournalController> logger, JournalDbContext context)
        {
            _logger = logger;
            _context = context; // gán database vào biến(_context) đã tạo
        }


        [HttpGet]

        public async Task<IActionResult> Get([FromQuery] Guid? id, [FromQuery] string? content, [FromQuery] DateTime? date,
            [FromQuery] int? pageSize, [FromQuery] int? pageIndex)// phải có dấu ? sau mỗi property, cho phép để trống chúng khi Get, nếu không sẽ lỗi
        {
            var journeys = _context.Journeys.AsQueryable(); //lấy table ra, nhưng chưa đâm xuống Database
            ////Query ID
            if (id.HasValue)
                journeys = journeys.Where(x => x.Id == id);
            ////Query Content
            if (!string.IsNullOrEmpty(content))
                journeys = journeys.Where(x => x.Content.Contains(content));
            ////Query Date
            if (date.HasValue)
                journeys = journeys.Where(x => x.Date == date);

            //chia trang
            if (pageSize.HasValue && pageIndex.HasValue && pageSize > 0 && pageIndex >= 0)
                journeys = journeys.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);

            var result = await journeys.AsNoTracking().ToListAsync();
            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromBody] Payload payload)
        {
            var journey = new Journey //tạo một hàng dữ liệu mới
            {
                Id = Guid.NewGuid(),
                Content = payload.Content,
                Date = payload.Date
            };
            _context.Journeys.Add(journey); // add hàng dữ liệu mới vào table
            await _context.SaveChangesAsync(); // lưu lại table
            //return CreatedAtRoute("Get Journey", new { id = journey.Id }, journey); // bắn ra http 201 created. id để có thể Get dữ liệu vừa tạo thêm
            return CreatedAtAction(nameof(Get), journey.Id);
        }

        [HttpDelete]

        public async Task<IActionResult> Delete([FromQuery] Guid id, [FromQuery] bool deleteNotes) // bắt buộc phải có id để tìm nên không cần dấu ?
        {
            // 
            
            var journey = await _context.Journeys.FindAsync(id);// chờ để ASP.NET tìm và lấy ra data với id được cho, hàm FindAsync chỉ để tìm khóa chính của bảng
            // tìm theo ngày??
            if (journey == null) //nếu data vẫn là null sau khi tìm nghĩa là không có data với id được cho
            {
                return NotFound();
            }
            _context.Journeys.Remove(journey); //xóa data tìm được khỏi table hiện tại
            if (deleteNotes)
            {
                await _context.Notes.Where(x => x.JourneyId == id).ExecuteDeleteAsync(); // gom de xoa
            }
            await _context.SaveChangesAsync();
            return NoContent(); //201
        }

        [HttpPut]

        public async Task<IActionResult> Put([FromBody] UpdatePayload payload)
            // đây là update trực tiếp từ dữ liệu người dùng nhập vào, còn một kiểu là update object cũ(có thể là bị lỗi dữ liệu) với object mới(object cũ với dữ liệu đúng)
                                                                        // là nên check thêm id của object mới so với cũ để chắc chắn rằng mình đã update đúng object bị lỗi?
        {
            var journey = await _context.Journeys.FindAsync(payload.Id);
            if (journey == null)
            {
                return NotFound();
            }
            journey.Content = payload.Content; // cập nhật data cũ với data mà người dùng nhập vào
            journey.Date = payload.Date;
            _context.Journeys.Update(journey);
            await _context.SaveChangesAsync();
            return NoContent(); //201
        }
    }
}
