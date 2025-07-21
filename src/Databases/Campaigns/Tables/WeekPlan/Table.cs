namespace Journal.Databases.Campaigns.Tables.WeekPlan
{
    public class Table
    {
        public Guid Id { get; set; }

        public Guid WorkoutId { get; set; }

        public DateTime DateOfWeek { get; set; } // làm sao để nó chỉ lấy thứ?

        public DateTime Time { get; set; } // làm sao để nó chỉ lấy giờ?

        public int Rep { get; set; } // làm sao để nó chỉ lấy một trong hai Rep hoặc HoldingTime

        public int HoldingTime { get; set; }

        public int Set { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
