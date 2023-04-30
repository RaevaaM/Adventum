using System.ComponentModel;

namespace Adventum.Data
{
    public class SportActivity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Short Description")]
        public string Description { get; set; }

        [DisplayName("Video URL")]
        public string VideoURL { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
