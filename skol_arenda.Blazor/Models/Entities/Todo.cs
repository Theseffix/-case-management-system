using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ITHSManagement.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        [ForeignKey("TaskItem")]
        public int TaskID { get; set; }
        public TaskItem TaskItem { get; set; }
        //public TodoConnection Connection { get; set; }
    }
}
