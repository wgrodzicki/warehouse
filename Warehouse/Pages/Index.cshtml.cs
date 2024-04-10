using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Warehouse.Data;
using static Warehouse.Helpers.Helpers;

namespace Warehouse.Pages;

public class IndexModel : PageModel
{
	private IConfiguration _configuration; 
    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;

        if (AccessData)
        {
			CurrentUserRole = UserRole.Coordinator;
			AccessData = false;
		}
        else
        {
			CurrentUserRole = UserRole.None;
		}
    }

	public void OnGet()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();

            // Pre-populate item groups
            List<string> itemGroups = new List<string>();
            WarehouseRepository.GetItemGroupNames(connection, itemGroups);
            if (itemGroups.Count == 0)
				WarehouseRepository.PopulateItemGroups(connection);

			// Pre-populate units
			List<string> units = new List<string>();
            WarehouseRepository.GetUnitNames(connection, units);
            if (units.Count == 0)
				WarehouseRepository.PopulateUnits(connection);

			// Pre-populate request statuses
			List<string> requestStatuses = new List<string>();
			WarehouseRepository.GetRequestStatusNames(connection, requestStatuses);
			if (requestStatuses.Count == 0)
				WarehouseRepository.PopulateRequestStatuses(connection);
		}
    }

    public IActionResult OnPostCoordinator()
    {
        CurrentUserRole = UserRole.Coordinator;
        return Page();
        
    }

    public IActionResult OnPostEmployee()
    {
        CurrentUserRole = UserRole.Employee;
        return new RedirectToPageResult("Items");
    }
}
