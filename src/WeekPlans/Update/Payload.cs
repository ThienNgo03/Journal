namespace Journal.WeekPlans.Update
{
    public class Payload
    {
        public Guid Id { get; set; }

        public Guid WorkoutId { get; set; }

        public DateTime DateOfWeek { get; set; }

        public DateTime Time { get; set; }

        public int Rep { get; set; }

        public int HoldingTime { get; set; }

        public int Set { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
