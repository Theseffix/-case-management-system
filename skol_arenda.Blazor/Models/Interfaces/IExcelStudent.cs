using Ganss.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITHSManagement.Models
{
    public class IExcelStudent
    {
        public List<ExcelStudent> MassImportStudent()
        {


            var products = new ExcelMapper("wwwroot/db.xlsx").Fetch<ExcelStudent>();
            foreach (var thing in products)
            {
                Console.WriteLine(thing.FirstName);
            }

            return products.ToList();
        }
    }

    public class ExcelStudent
    {
        [Column("Status")]
        public string Status { get; set; }
        [Column("Födelseår/orgnr")]
        public string BirthDay { get; set; }
        [Column("Förnamn")]
        public string FirstName { get; set; }
        [Column("Efternamn")]
        public string LastName { get; set; }
        [Column("Telefon")]
        public string Phone { get; set; }
        [Column("e-mail")]
        public string Email { get; set; }
        [Column("Gautadress")]
        public string Street { get; set; }
        [Column("Postnummer")]
        public string Zipcode { get; set; }
        [Column("Ort")]
        public string City { get; set; }


    }
}
