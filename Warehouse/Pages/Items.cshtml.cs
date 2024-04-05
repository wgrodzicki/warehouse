using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Warehouse.Models;
using Warehouse.Data;

namespace Warehouse.Pages;

public class ItemsModel : PageModel
{
    [BindProperty] public List<ItemModel> Items { get; set; }
    private IConfiguration _configuration;

    public ItemsModel(IConfiguration configuration)
    {
        _configuration = configuration;
        Items = new List<ItemModel>();
    }

    public IActionResult OnGet()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            WarehouseRepository.GetAllItems(connection, Items);
        }
        return Page();
    }
}
