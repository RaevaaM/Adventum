using System.ComponentModel.DataAnnotations;

namespace Adventum.Data
{
    public class EventReservation
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        [Display(Name = "Участници")]
        public int ParticipantsCount { get; set; }

        public virtual User User { get; set; }

        public virtual Event Event { get; set; }
    }
}
