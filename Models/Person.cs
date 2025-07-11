namespace Workers.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsManager { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}
