using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITHSManagement.Models
{
    public class Student : Person
    {
        public ICollection<Programme> Programme { get; set; }
        public ICollection<Course> Course { get; set; }
        public ICollection<Group> Group { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<LIAWork> LIAWork { get; set; }
    }
}