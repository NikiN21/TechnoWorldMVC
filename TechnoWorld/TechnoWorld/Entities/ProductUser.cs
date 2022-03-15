using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoWorld.Entities
{
    public class ProductUser: IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
