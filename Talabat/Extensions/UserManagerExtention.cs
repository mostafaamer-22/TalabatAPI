using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.DAL.Entities.Identity;

namespace Talabat.Extensions
{
    public static class UserManagerExtention
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager , ClaimsPrincipal claims)
        {
            var email  = claims.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(e => e.Address).SingleOrDefaultAsync(p => p.Email == email);
            return user;
        }
    }
}
