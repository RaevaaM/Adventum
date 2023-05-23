using System.ComponentModel.DataAnnotations;

namespace Adventum.Data
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Местоположение")]
        public string Name { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}

