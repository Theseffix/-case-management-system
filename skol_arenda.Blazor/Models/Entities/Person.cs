using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITHSManagement.Models
{
    public class Person
    {

        public int Id { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public ICollection<ContactInfo> ContactInfos { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
        public ICollection<Todo> Todos { get; set; }

    }
}
