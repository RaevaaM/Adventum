using Adventum.Data;
using Microsoft.Build.Framework;

namespace Adventum.Models
{
    public class SportActivityVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoURL { get; set; }
        public ICollection<Event> Events { get; set; }

    }
}
