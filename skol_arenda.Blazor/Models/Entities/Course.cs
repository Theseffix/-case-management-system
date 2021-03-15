using System;
using System.Collections.Generic;

namespace ITHSManagement.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string CourseAdmin { get; set; }
        public ICollection<CompRep> CompanyReps { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Programme> Programmes { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
        public ICollection<Todo> Todos { get; set; }
        public DateTime? StartDeviation { get; set; }
        public DateTime? EndDeviation { get; set; }
        public string DeviationDescription { get; set; }
    }
}
