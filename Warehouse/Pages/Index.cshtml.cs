using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Warehouse.Roles;

namespace Warehouse.Pages;

public class IndexModel : PageModel
{
    public bool CoordinatorRoleChosen { get; set; } = false;

    public void OnGet()
    {

    }

    public IActionResult OnPostCoordinator()
    {
        Role.CurrentUserRole = Role.UserRole.Coordinator;
        CoordinatorRoleChosen = true;
        return Page();
        
    }

    public IActionResult OnPostEmployee()
    {
        Role.CurrentUserRole = Role.UserRole.Employee;
        return new RedirectToPageResult("Items");
    }
}
