using System.Collections.Generic;

namespace Workers.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Person> People { get; set; } = new();
    }
}
