using System;
using System.Collections.Generic;

namespace ITHSManagement.Models
{
    public class Programme
    {
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public string YhNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string CourseAdmin { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<CompRep> CompanyReps { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
        public ICollection<Todo> Todos { get; set; }

    }
}
