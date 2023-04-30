using System.ComponentModel.DataAnnotations.Schema;

namespace Adventum.Data
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(SportActivity))]
        public int SportActivityId { get; set; }


        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }

        public virtual SportActivity SportActivity { get; set; }
        public virtual Location Location { get; set; }

        public ICollection<EventReservation> EventReservations { get; set; }
    }
}
