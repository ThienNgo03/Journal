﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Journal.Databases.Campaigns.Tables.Journey
{
    public class Table
    {

        public Guid Id { get; set; } // cột ID

        public string Content { get; set; } // cột nội dung nhật ký

        public string? Location { get; set; }

        public DateTime Date { get; set; } // cột ngày viết nhật ký này
    }
}
