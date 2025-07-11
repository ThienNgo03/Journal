using System.ComponentModel.DataAnnotations;

namespace Journal
{
    public class Payload // class đại diện cho những thứ mà user sẽ đưa lên server
    {

        // không cần nhập id vì id sẽ do hệ thống tự tạo trong controller

        [Required] //Required ở đây?
        public string Content { get; set; } = string.Empty; // nội dung nhật ký, Required ở đây?

        [Required]
        public DateTime Date { get; set; } // ngày viết nhật ký này

    }
}
