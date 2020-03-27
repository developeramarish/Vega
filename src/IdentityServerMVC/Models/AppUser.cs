using Microsoft.AspNetCore.Identity;

namespace IdentityServerMVC.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
