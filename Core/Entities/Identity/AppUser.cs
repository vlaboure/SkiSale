using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser: IdentityUser
    {
        //une adresse par personne
        public string DisplayName { get; set; }
        public Adress Adress { get; set; }                                
    }
}