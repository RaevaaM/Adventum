namespace Adventum.Data
{
    public class SportActivity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string VideoURL { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
