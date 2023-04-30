using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace Adventum.Data
{
    public class User:IdentityUser
    {

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        public virtual ICollection<EventReservation> EventReservations { get; set; }
    }
}
