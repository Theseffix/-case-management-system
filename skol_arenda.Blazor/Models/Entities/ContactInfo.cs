using System.ComponentModel.DataAnnotations;

namespace ITHSManagement.Models
{
    public class ContactInfo
    {
        //Klar 2020-02-02
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public Person User { get; set; }
        public ContactTypeEnum ContactType { get; set; }
        public string Contact { get; set; }
    }
}
