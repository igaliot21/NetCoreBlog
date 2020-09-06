using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlog.Models
{
    public class AppUser : IdentityUser
    {
        public string Surname { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}