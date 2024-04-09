using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Warehouse.Models;
using Warehouse.Data;
using Warehouse.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Warehouse.Pages;

public class ItemsModel : PageModel
{
    [BindProperty] public List<Helpers.Helpers.ItemToDisplay> ItemsToDisplay { get; set; }
	[BindProperty] public Helpers.Helpers.ItemToDisplay ItemToAdd { get; set; }
	[BindProperty] public Helpers.Helpers.ItemToDisplay ItemToUpdate { get; set; }
	[BindProperty] public RequestModel PurchaseRequest { get; set; }
	[BindProperty] public int ItemIdToDelete { get; set; }
	[BindProperty] public string ItemNameToSearchFor { get; set; }
	[BindProperty] public string SortingMode { get; set; }

	public List<string> ItemGroupNames { get; set; }
	public List<string> UnitNames { get; set; }
    public string AutoOpenAddItemModal { get; set; }
	public string AutoOpenEditItemModal { get; set; }
	public string AutoOpenOrderItemModal { get; set; }
	public string AutoOpenRequestConfirmModal { get; set; }
	public int TableRowCounter { get; set; } = 0;


	private IConfiguration _configuration;

    public ItemsModel(IConfiguration configuration)
    {
        _configuration = configuration;

        ItemsToDisplay = new List<Helpers.Helpers.ItemToDisplay>();
		ItemToAdd = new Helpers.Helpers.ItemToDisplay();
		ItemToUpdate = new Helpers.Helpers.ItemToDisplay();
		PurchaseRequest = new RequestModel();
		ItemGroupNames = new List<string>();
        UnitNames = new List<string>();

        AutoOpenAddItemModal = "no";
		AutoOpenEditItemModal = "no";
		AutoOpenOrderItemModal = "no";
		AutoOpenRequestConfirmModal = "no";
	}

    public IActionResult OnGet()
    {
		PopulateItems();

		return Page();
    }

    public IActionResult OnPostAddItem()
    {
        if (!ModelState.IsValid)
        {
			// Validate only the Add item form
			foreach (var state in ModelState)
			{
				if (state.Key.Contains("ItemToAdd") && state.Value.ValidationState == ModelValidationState.Invalid)
                {
					AutoOpenAddItemModal = "yes";
					return OnGet();
				}
			}
		}
		AutoOpenAddItemModal = "no";

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            int itemGroupId = WarehouseRepository.GetItemGroupIdByItemGroupName(connection, ItemToAdd.ItemGroup);
            int unitId = WarehouseRepository.GetUnitIdByUnitName(connection, ItemToAdd.Unit);

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
            WarehouseRepository.DeleteItem(connection, ItemIdToDelete);
        }
		return OnGet();
	}

    public IActionResult OnPostUpdateItem()
    {
		if (!ModelState.IsValid)
		{
			// Validate only the Edit item form
			foreach (var state in ModelState)
			{
				if (state.Key.Contains("ItemToUpdate") && state.Value.ValidationState == ModelValidationState.Invalid)
				{
					AutoOpenEditItemModal = "yes";
					return OnGet();
				}
			}
		}
		AutoOpenEditItemModal = "no";

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();

            int itemGroupId = WarehouseRepository.GetItemGroupIdByItemGroupName(connection, ItemToUpdate.ItemGroup);
            int unitId = WarehouseRepository.GetUnitIdByUnitName(connection, ItemToUpdate.Unit);

			if (itemGroupId < 0 || unitId < 0)
				return OnGet();

			ItemModel item = new ItemModel
			{
				Name = ItemToUpdate.Name,
				ItemGroupId = itemGroupId,
				UnitId = unitId,
				Quantity = ItemToUpdate.Quantity,
				PriceNoVat = ItemToUpdate.PriceNoVat,
				Status = ItemToUpdate.Status,
				StorageLocation = ItemToUpdate.StorageLocation,
				ContactPerson = ItemToUpdate.ContactPerson
			};

			WarehouseRepository.UpdateItem(connection, item, ItemToUpdate.Id);
        }
        return OnGet();
    }

	public IActionResult OnPostOrderItem()
	{
		if (!ModelState.IsValid)
		{
			// Validate only the Order item form
			foreach (var state in ModelState)
			{
				if (state.Key.Contains("PurchaseRequest") && state.Value.ValidationState == ModelValidationState.Invalid)
				{
					AutoOpenOrderItemModal = "yes";
					return OnGet();
				}
			}
		}
		AutoOpenOrderItemModal = "no";
		AutoOpenRequestConfirmModal = "yes";

		using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
		{
			connection.Open();

			PurchaseRequest.PriceNoVat = WarehouseRepository.GetItemPriceByItemId(connection, PurchaseRequest.ItemId) * PurchaseRequest.Quantity;

			int statusId = WarehouseRepository.GetRequestStatusIdByRequestStatusName(connection, "New");
			if (statusId > 0)
				PurchaseRequest.StatusId = statusId;
			else
				PurchaseRequest.StatusId = 1;

			WarehouseRepository.AddRequest(connection, PurchaseRequest);
		}

		return OnGet();
	}

	public IActionResult OnPostSearchItem()
	{
		if (String.IsNullOrEmpty(ItemNameToSearchFor))
		{
			return OnGet();
		}

		using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
		{
			connection.Open();
			List<ItemModel> items = new List<ItemModel>();
			WarehouseRepository.GetItemsByItemName(connection, ItemNameToSearchFor, items);

			foreach (ItemModel item in items)
			{
				ItemsToDisplay.Add(new Helpers.Helpers.ItemToDisplay
				{
					Id = item.Id,
					Name = item.Name,
					ItemGroup = WarehouseRepository.GetItemGroupNameByItemGroupId(connection, item.ItemGroupId),
					Unit = WarehouseRepository.GetUnitNameByUnitId(connection, item.UnitId),
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

    public IActionResult OnPostSortId()
    {
        PopulateItems();

		if (SortingMode == "↑")
            ItemsToDisplay = ItemsToDisplay.OrderBy(x => x.Id).ToList();
        else
            ItemsToDisplay = ItemsToDisplay.OrderByDescending(x => x.Id).ToList();

		return Page();
    }

	public IActionResult OnPostSortName()
	{
		PopulateItems();

		if (SortingMode == "↑")
			ItemsToDisplay = ItemsToDisplay.OrderBy(x => x.Name).ToList();
		else
			ItemsToDisplay = ItemsToDisplay.OrderByDescending(x => x.Name).ToList();

		return Page();
	}

	public IActionResult OnPostSortItemGroup()
	{
		PopulateItems();

		if (SortingMode == "↑")
			ItemsToDisplay = ItemsToDisplay.OrderBy(x => x.ItemGroup).ToList();
		else
			ItemsToDisplay = ItemsToDisplay.OrderByDescending(x => x.ItemGroup).ToList();

		return Page();
	}

	public IActionResult OnPostSortUnit()
	{
		PopulateItems();

		if (SortingMode == "↑")
			ItemsToDisplay = ItemsToDisplay.OrderBy(x => x.Unit).ToList();
		else
			ItemsToDisplay = ItemsToDisplay.OrderByDescending(x => x.Unit).ToList();

		return Page();
	}

	public IActionResult OnPostSortQuantity()
	{
		PopulateItems();

		if (SortingMode == "↑")
			ItemsToDisplay = ItemsToDisplay.OrderBy(x => x.Quantity).ToList();
		else
			ItemsToDisplay = ItemsToDisplay.OrderByDescending(x => x.Quantity).ToList();

		return Page();
	}

	public IActionResult OnPostSortPrice()
	{
		PopulateItems();

		if (SortingMode == "↑")
			ItemsToDisplay = ItemsToDisplay.OrderBy(x => x.PriceNoVat).ToList();
		else
			ItemsToDisplay = ItemsToDisplay.OrderByDescending(x => x.PriceNoVat).ToList();

		return Page();
	}

	public IActionResult OnPostSortStatus()
	{
		PopulateItems();

		if (SortingMode == "↑")
			ItemsToDisplay = ItemsToDisplay.OrderBy(x => x.Status).ToList();
		else
			ItemsToDisplay = ItemsToDisplay.OrderByDescending(x => x.Status).ToList();

		return Page();
	}

	public IActionResult OnPostSortStorage()
	{
		PopulateItems();

		if (SortingMode == "↑")
			ItemsToDisplay = ItemsToDisplay.OrderBy(x => x.StorageLocation).ToList();
		else
			ItemsToDisplay = ItemsToDisplay.OrderByDescending(x => x.StorageLocation).ToList();

		return Page();
	}

	public IActionResult OnPostSortContact()
	{
		PopulateItems();

		if (SortingMode == "↑")
			ItemsToDisplay = ItemsToDisplay.OrderBy(x => x.ContactPerson).ToList();
		else
			ItemsToDisplay = ItemsToDisplay.OrderByDescending(x => x.ContactPerson).ToList();

		return Page();
	}

	/// <summary>
	/// Populates dropdown lists and the list of items to be displayed in the main table.
	/// </summary>
	private void PopulateItems()
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
					ItemGroup = WarehouseRepository.GetItemGroupNameByItemGroupId(connection, item.ItemGroupId),
					Unit = WarehouseRepository.GetUnitNameByUnitId(connection, item.UnitId),
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
	}
}
