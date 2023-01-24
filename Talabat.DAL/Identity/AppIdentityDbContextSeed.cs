using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace Talabat.DAL.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Mostafa Amer",
                    UserName = "mostafa123",
                    Email = "Mos@gmail.com",
                    PhoneNumber = "0156763621",
                    Address = new Address()
                    {
                        FirstName = "Mostafa",
                        LastName = "Amer",
                        Country = "Egypt",
                        City = "Cairo",
                        Street = "10 Dokki",
                        ZipCode = "1213"
                    }

                };
                await userManager.CreateAsync(user , "Pa$$w0rd");
            }
        }
    }
}
