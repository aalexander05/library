using LibraryApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryApi.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Identity.Bearer")]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPut("BecomeCustomer")]
    public async Task<IActionResult> BecomeCustomer()
    {
        ApplicationUser user = await _userManager.GetUserAsync(User);

        var result = await _userManager.AddToRoleAsync(user, "Customer");

        return NoContent();
    }

    [HttpPut("BecomeLibrarian")]
    public async Task<IActionResult> BecomeLibrarian()
    {
        ApplicationUser user = await _userManager.GetUserAsync(User);

        var result = await _userManager.AddToRoleAsync(user, "Librarian");

        return NoContent();
    }


    [HttpGet]
    public async Task<IEnumerable<string>> Get()
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        ApplicationUser user = await _userManager.FindByIdAsync(userId);
        IEnumerable<string> role = await _userManager.GetRolesAsync(user);
        return role;
    }
}
