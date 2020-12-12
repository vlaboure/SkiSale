
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Identity
{
    public class Adress
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        [Required]
        public string AppUserId { get; set; }
        
        
        public AppUser AppUser { get; set; }
                          
    }
}