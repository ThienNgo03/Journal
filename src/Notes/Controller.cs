using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Journal.Notes
{
    [ApiController]
    [Route("Notes")]
    public class Controller : ControllerBase
    {

        private readonly ILogger<Controller> _logger;

        private readonly JournalDbContext _context; //biến đại diện cho database

        public Controller(ILogger<Controller> logger, JournalDbContext context)
        {
            _logger = logger;
            _context = context; // gán database vào biến(_context) đã tạo
        }
        [HttpGet]

        public async Task<IActionResult> Get([FromQuery] Get.Parameters parameters)// phải có dấu ? sau mỗi property, cho phép để trống chúng khi Get, nếu không sẽ lỗi
        {
            var query = _context.Notes.AsQueryable(); //lấy table ra, nhưng chưa đâm xuống Database
            ////Query ID
            if (parameters.id.HasValue)
                query = query.Where(x => x.Id == parameters.id);
            ////Query IDJourney
            if (parameters.journeyId.HasValue)
                query = query.Where(x => x.JourneyId == parameters.journeyId);
            ////Query Content
            if (!string.IsNullOrEmpty(parameters.content))
                query = query.Where(x => x.Content.Contains(parameters.content));
            ////Query Date
            if (parameters.date.HasValue)
                query = query.Where(x => x.Date == parameters.date);

            ////Query Mood
            if (!string.IsNullOrEmpty(parameters.mood))
                query = query.Where(x => x.Mood.Contains(parameters.mood));

            //chia trang
            if (parameters.pageSize.HasValue && parameters.pageIndex.HasValue && parameters.pageSize > 0 && parameters.pageIndex >= 0)
                query = query.Skip(parameters.pageIndex.Value * parameters.pageSize.Value).Take(parameters.pageSize.Value);

            var result = await query.AsNoTracking().ToListAsync();
            return Ok(result);

        }

        [HttpPost]

        public async Task<IActionResult> Post([FromBody] Post.Payload payload)
        {
            var journeyId = payload.JourneyId;
            var journey = await _context.Journeys.FirstOrDefaultAsync(x => x.Id == journeyId);
            if (journey == null)
            {
                return NotFound();
            }
            var note = new Table //tạo một hàng dữ liệu mới
            {
                Id = Guid.NewGuid(),
                JourneyId = payload.JourneyId,
                Content = payload.Content,
                Date = payload.Date,
                Mood = payload.Mood
            };
            _context.Notes.Add(note); // add hàng dữ liệu mới vào table
            await _context.SaveChangesAsync(); // lưu lại table
            //return CreatedAtRoute("Get Journey", new { id = journey.Id }, journey); // bắn ra http 201 created. id để có thể Get dữ liệu vừa tạo thêm
            return CreatedAtAction(nameof(Get), note.Id);
        }
        [HttpDelete]

        public async Task<IActionResult> Delete([FromQuery] Delete.Parameters parameters) // bắt buộc phải có id để tìm nên không cần dấu ?
        {

            var note = await _context.Notes.FindAsync(parameters.Id);// chờ để ASP.NET tìm và lấy ra data với id được cho, hàm FindAsync chỉ để tìm khóa chính của bảng
            // tìm theo ngày??
            if (note == null) //nếu data vẫn là null sau khi tìm nghĩa là không có data với id được cho
            {
                return NotFound();
            }
            _context.Notes.Remove(note); //xóa data tìm được khỏi table hiện tại
            await _context.SaveChangesAsync();
            return NoContent(); //201
        }

        [HttpPut]

        public async Task<IActionResult> Put([FromBody] Update.Payload payload)
        // đây là update trực tiếp từ dữ liệu người dùng nhập vào, còn một kiểu là update object cũ(có thể là bị lỗi dữ liệu) với object mới(object cũ với dữ liệu đúng)
        // là nên check thêm id của object mới so với cũ để chắc chắn rằng mình đã update đúng object bị lỗi?
        {
            // Kiếm Journey-Id
            var journeyId = payload.JourneyId;
            var journey = await _context.Journeys.FirstOrDefaultAsync(x => x.Id == journeyId);
            if (journey == null)
            {
                return NotFound();
            }
            var note = await _context.Notes.FindAsync(payload.Id);
            if (note == null)
            {
                return NotFound();
            }
            note.Content = payload.Content; // cập nhật data cũ với data mà người dùng nhập vào
            note.Date = payload.Date;
            note.Mood = payload.Mood;
            note.JourneyId = payload.JourneyId;
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return NoContent(); //201
        }

    }
}
