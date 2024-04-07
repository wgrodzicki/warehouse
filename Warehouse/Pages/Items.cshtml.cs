using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Warehouse.Models;
using Warehouse.Data;
using Warehouse.Helpers;

namespace Warehouse.Pages;

public class ItemsModel : PageModel
{
    [BindProperty] public List<Helpers.Helpers.ItemToDisplay> ItemsToDisplay { get; set; }
	[BindProperty] public Helpers.Helpers.ItemToDisplay ItemToAdd { get; set; }
    [BindProperty] public Helpers.Helpers.ItemToDisplay ItemToUpdate { get; set; }
	[BindProperty] public int ItemToDeleteId { get; set; }

    public List<string> ItemGroupNames { get; set; }
	public List<string> UnitNames { get; set; }
    public string AutoOpenAddItemModal { get; set; }

	private IConfiguration _configuration;

    public ItemsModel(IConfiguration configuration)
    {
        _configuration = configuration;
        ItemsToDisplay = new List<Helpers.Helpers.ItemToDisplay>();
		ItemToAdd = new Helpers.Helpers.ItemToDisplay();
		ItemGroupNames = new List<string>();
        UnitNames = new List<string>();
        AutoOpenAddItemModal = "no";
    }

    public IActionResult OnGet()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            List<ItemModel> items = new List<ItemModel>();
            WarehouseRepository.GetAllItems(connection, items);

            foreach (ItemModel item in items)
            {
                ItemsToDisplay.Add(new Helpers.Helpers.ItemToDisplay
                {
                    Id = item.Id,
                    Name = item.Name,
                    ItemGroup = WarehouseRepository.GetItemGroupNameById(connection, item.ItemGroupId),
                    Unit = WarehouseRepository.GetUnitNameById(connection, item.UnitId),
                    Quantity = item.Quantity,
                    PriceNoVat = item.PriceNoVat,
                    Status = item.Status,
                    StorageLocation = item.StorageLocation,
                    ContactPerson = item.ContactPerson,
                });
            }

            WarehouseRepository.GetItemGroupNames(connection, ItemGroupNames);
            WarehouseRepository.GetUnitNames(connection, UnitNames);
        }
        return Page();
    }
    
    public IActionResult OnPostAddItem()
    {
        if (!ModelState.IsValid)
        {
			AutoOpenAddItemModal = "yes";
            return OnGet();
		}
		AutoOpenAddItemModal = "no";

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            int itemGroupId = WarehouseRepository.GetItemGroupIdByName(connection, ItemToAdd.ItemGroup);
            int unitId = WarehouseRepository.GetUnitIdByName(connection, ItemToAdd.Unit);

            if (itemGroupId < 0 || unitId < 0)
                return OnGet();

            ItemModel item = new ItemModel
            {
                Name = ItemToAdd.Name,
                ItemGroupId = itemGroupId,
                UnitId = unitId,
                Quantity = ItemToAdd.Quantity,
                PriceNoVat = ItemToAdd.PriceNoVat,
                Status = ItemToAdd.Status,
                StorageLocation = ItemToAdd.StorageLocation,
                ContactPerson = ItemToAdd.ContactPerson
            };

            WarehouseRepository.AddItem(connection, item);
        }
	    return OnGet();
	}

    public IActionResult OnPostDeleteItem()
    {
		using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            WarehouseRepository.DeleteItem(connection, ItemToDeleteId);
        }
        return OnGet();
	}
}
