namespace Journal
{
    public class UpdatePayload
    {
        public Guid Id { get; set; } // cột ID

        public string Content { get; set; } = string.Empty; // cột nội dung nhật ký

        public DateTime Date { get; set; } // cột ngày viết nhật ký này
    }
}
