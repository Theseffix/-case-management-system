using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITHSManagement.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Person Person { get; set; }
    }
}
