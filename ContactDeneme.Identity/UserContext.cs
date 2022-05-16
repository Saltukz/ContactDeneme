
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Identity
{
    public class UserContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
    }
}
