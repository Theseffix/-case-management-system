using System.Collections.Generic;

namespace ITHSManagement.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Person> Members { get; set; }
        public ICollection<Programme> Programmes { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
        public ICollection<Todo> Todos { get; set; }

    }
}
