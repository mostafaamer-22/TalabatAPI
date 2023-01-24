using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace Talabat.DAL.Identity
{
    public class AppIdentityContextDb : IdentityDbContext<AppUser>
    {
        public AppIdentityContextDb(DbContextOptions<AppIdentityContextDb> options) : base(options)
        {

        }
    }
}
