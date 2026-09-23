using Microsoft.AspNetCore.Identity;

namespace school.Models
{
    public class Users:IdentityUser
    {

        public string FullName { get; set; }
    }
}
