using System;

namespace ITHSManagement.Models
{
    public class LIAWork
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Company Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
