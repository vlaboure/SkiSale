using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class UserManageExtension
    {
        #region 
        /*
            Extension pour retourner un user ou une adresse par claimPrincipal
        */
        #endregion

        private static string ReturnEmail(ClaimsPrincipal user){
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
        public static async Task<AppUser> FindByUserClaimPrincipalWithAdressAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = ReturnEmail(user);
            
            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser> FindEmailFromClaimPrincipal(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            // HttpContext.User passé en paramètre est un ClaimsPrincipal
            var email = ReturnEmail(user);
           
            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}