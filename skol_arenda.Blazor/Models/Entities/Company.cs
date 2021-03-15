using System.Collections.Generic;

namespace ITHSManagement.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string OrganisationNumber { get; set; }
        public string Name { get; set; }
        public ICollection<LIAWork> LIAWork { get; set; }
        public ICollection<CompRep> CompReps { get; set; }
    }
}
