namespace Adventum.Data
{
    public class Location
    {
        public int Id { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longtitude { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}

