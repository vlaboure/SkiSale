using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                     DisplayName = "Vince",
                    Email = "vince@test.com",
                    UserName = "vince@test.com",
                    Adress= new Adress
                    {
                        FirstName = "Vincent",
                        Name = "Labouré",
                        Street = "16/43 Rue Victor Hugo",
                        City = "Tourcoing",
                        State = "France",
                        ZipCode = "59200"
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}