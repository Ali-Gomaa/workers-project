﻿namespace Workers.Models
{
    public class JobTitle
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Person> People { get; set; }
    }

}
