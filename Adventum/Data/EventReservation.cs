namespace Adventum.Data
{
    public class EventReservation
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Event Event { get; set; }
    }
}
