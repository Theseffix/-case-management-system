using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITHSManagement.Models
{
    public class TaskComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; } 
        [ForeignKey("TaskItem")]
        public int TaskID { get; set; }
        public TaskItem TaskItem { get; set; }
        //public TodoConnection Connection { get; set; }
    }
}
