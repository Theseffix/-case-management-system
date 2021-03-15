using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITHSManagement.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; } //How imprtant (1-5)
        public int Consequence { get; set; } //How bad if not finished (1-5)
        public int UpdateCount { get; set; }
        public string Status { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public int? ProgrammeId { get; set; }
        public Programme Programme { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }
        public int? GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<Todo> Todos { get; set; }
        public ICollection<TaskComment> TaskComments { get; set; }
    }
}
