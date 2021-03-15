using System.ComponentModel.DataAnnotations.Schema;

namespace ITHSManagement.Models
{
    public class Address
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Person User { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

    }
}
