using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsLibrarian { get; set; }
    }
}
