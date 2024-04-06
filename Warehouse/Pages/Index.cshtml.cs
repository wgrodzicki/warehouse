using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Warehouse.Data;
using Warehouse.Helpers;

namespace Warehouse.Pages;

public class IndexModel : PageModel
{
    public bool CoordinatorRoleChosen { get; set; } = false;
	private IConfiguration _configuration;

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

	public void OnGet()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();

            List<string> itemGroups = new List<string>();
            WarehouseRepository.GetItemGroupNames(connection, itemGroups);
            if (itemGroups.Count == 0)
				WarehouseRepository.PopulateItemGroups(connection);

			List<string> units = new List<string>();
            WarehouseRepository.GetUnitNames(connection, units);
            if (units.Count == 0)
				WarehouseRepository.PopulateUnits(connection);
		}
    }

    public IActionResult OnPostCoordinator()
    {
        Helpers.Helpers.CurrentUserRole = Helpers.Helpers.UserRole.Coordinator;
        CoordinatorRoleChosen = true;
        return Page();
        
    }

    public IActionResult OnPostEmployee()
    {
        Helpers.Helpers.CurrentUserRole = Helpers.Helpers.UserRole.Employee;
        return new RedirectToPageResult("Items");
    }
}
