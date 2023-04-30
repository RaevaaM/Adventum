using System.ComponentModel;
using Adventum.Data;

namespace Adventum.Models;

public class EventReservationModel
{
    public int EventReservationId { get; set; }
    
    [DisplayName("User Name")]
    public string UserFullName { get; set; }
    
    [DisplayName("Event Name")]
    public string EventName { get; set; }
    
    [DisplayName("Activity Location")]
    public Location Location { get; set; }
}