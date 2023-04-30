using Microsoft.AspNetCore.Mvc.Rendering;

namespace Adventum.Models
{
    public class EventReservationsVM
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public List<SelectListItem> Events { get; set; }
        public int UserId { get; set; }
        //public List<SelectListItem> Users { get; set; }

    }
}
