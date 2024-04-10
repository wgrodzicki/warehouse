using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Warehouse.Models;
using Warehouse.Data;
using static Warehouse.Helpers.Helpers;

namespace Warehouse.Pages;

public class RequestsModel : PageModel
{
	[BindProperty] public List<RequestToDisplay> RequestsToDisplay { get; set; }
	[BindProperty] public RequestToDisplay RequestToManage { get; set; }
	[BindProperty] public int RequestIdToSearchFor { get; set; }
	[BindProperty] public string SortingMode { get; set; }
	public List<string> RequestStatusNames { get; set; }
	public string AutoOpenRequestModal { get; set; }
	public string AutoOpenRequestRefusalModal { get; set; }
	public int TableRowCounter { get; set; } = 0;


	private IConfiguration _configuration;

	public RequestsModel(IConfiguration configuration)
	{
		_configuration = configuration;

		RequestsToDisplay = new List<RequestToDisplay>();
		RequestToManage = new RequestToDisplay();
		RequestStatusNames = new List<string>();

		AutoOpenRequestModal = "no";
		AutoOpenRequestRefusalModal = "no";
	}

	public IActionResult OnGet()
    {
		PopulateRequests();

		CurrentlyDisplayedRequests = RequestsToDisplay;

		return Page();
    }

    public IActionResult OnPostAccessData()
    {
		AccessData = true;
		return new RedirectToPageResult("Index");
    }

    public IActionResult OnPostManageRequest()
	{
		using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
		{
			connection.Open();

			// Get item id
			string itemIdTextual = "";
			for (int i = 1; i < RequestToManage.Item.Length; i++)
			{
				if (RequestToManage.Item[i].ToString() == " ")
					break;

				itemIdTextual += RequestToManage.Item[i];
			}
			int itemId = int.Parse(itemIdTextual);

			int statusId = WarehouseRepository.GetRequestStatusIdByRequestStatusName(connection, RequestToManage.Status);

			if (itemId < 0 || statusId < 0)
				return OnGet();

			RequestModel request = new RequestModel
			{
				Id = RequestToManage.Id,
				ItemId = itemId,
				EmployeeName = RequestToManage.EmployeeName,
				Quantity = RequestToManage.Quantity,
				PriceNoVat = RequestToManage.TotalPriceNoVat,
				CommentEmployee = RequestToManage.CommentEmployee,
				CommentCoordinator = RequestToManage.CommentCoordinator,
				StatusId = statusId
			};

			WarehouseRepository.UpdateRequest(connection, request, RequestToManage.Id);

			switch (RequestToManage.Status)
			{
				case "Accepted":
					break;
				case "New":
					return OnGet();
				case "Rejected":
					return OnGet();
				default:
					return OnGet();
			}

			// Reduce the available quantity of the item
			ItemModel itemToUpdate = WarehouseRepository.GetItemByItemId(connection, request.ItemId);
			itemToUpdate.Quantity -= request.Quantity;

			if (itemToUpdate.Quantity < 0)
			{
				request.StatusId = WarehouseRepository.GetRequestStatusIdByRequestStatusName(connection, "New");
				WarehouseRepository.UpdateRequest(connection, request, request.Id);
				AutoOpenRequestRefusalModal = "yes";
				return OnGet();
			}
				
			WarehouseRepository.UpdateItem(connection, itemToUpdate, request.ItemId);
		}
		return OnGet();
	}

	public IActionResult OnPostSearchRequest()
	{
		int requestId = 0;
		int.TryParse(RequestIdToSearchFor.ToString(), out requestId);
		
		if (requestId == 0)
			return OnGet();

		using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
		{
			connection.Open();
			RequestModel request = new RequestModel();
			WarehouseRepository.GetRequestByRequestId(connection, requestId, request);
			RequestsToDisplay.Add(new RequestToDisplay
			{
				Id = request.Id,
				EmployeeName = request.EmployeeName,
				Item = $"#{request.ItemId} {WarehouseRepository.GetItemNameByItemId(connection, request.ItemId)}",
				Unit = WarehouseRepository.GetUnitNameByItemId(connection, request.ItemId),
				Quantity = request.Quantity,
				TotalPriceNoVat = request.PriceNoVat,
				CommentEmployee = request.CommentEmployee,
				CommentCoordinator = request.CommentCoordinator,
				Status = WarehouseRepository.GetRequestStatusNameByRequestStatusId(connection, request.StatusId)
			});

			WarehouseRepository.GetRequestStatusNames(connection, RequestStatusNames);
		}

		if (RequestsToDisplay[0].Id <= 0)
			RequestsToDisplay.Clear();

		CurrentlyDisplayedRequests = RequestsToDisplay;

		return Page();
	}

	public IActionResult OnPostSortId()
	{
		RequestsToDisplay = CurrentlyDisplayedRequests;

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.Id).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.Id).ToList();
		return Page();
	}

	public IActionResult OnPostSortEmployeeName()
	{
		RequestsToDisplay = CurrentlyDisplayedRequests;

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.EmployeeName).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.EmployeeName).ToList();
		return Page();
	}

	public IActionResult OnPostSortItem()
	{
		RequestsToDisplay = CurrentlyDisplayedRequests;

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.Item).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.Item).ToList();
		return Page();
	}

	public IActionResult OnPostSortUnit()
	{
		RequestsToDisplay = CurrentlyDisplayedRequests;

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.Unit).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.Unit).ToList();
		return Page();
	}

	public IActionResult OnPostSortQuantity()
	{
		RequestsToDisplay = CurrentlyDisplayedRequests;

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.Quantity).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.Quantity).ToList();
		return Page();
	}

	public IActionResult OnPostSortPrice()
	{
		RequestsToDisplay = CurrentlyDisplayedRequests;

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.TotalPriceNoVat).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.TotalPriceNoVat).ToList();
		return Page();
	}

	public IActionResult OnPostSortCommentEmployee()
	{
		RequestsToDisplay = CurrentlyDisplayedRequests;

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.CommentEmployee).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.CommentEmployee).ToList();
		return Page();
	}

	public IActionResult OnPostSortCommentCoordinator()
	{
		RequestsToDisplay = CurrentlyDisplayedRequests;

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.CommentCoordinator).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.CommentCoordinator).ToList();
		return Page();
	}

	public IActionResult OnPostSortStatus()
	{
		RequestsToDisplay = CurrentlyDisplayedRequests;

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.Status).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.Status).ToList();
		return Page();
	}

	private void PopulateRequests()
	{
		using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
		{
			connection.Open();
			List<RequestModel> requests = new List<RequestModel>();
			WarehouseRepository.GetAllRequests(connection, requests);

			foreach (RequestModel request in requests)
			{
				RequestsToDisplay.Add(new RequestToDisplay
				{
					Id = request.Id,
					EmployeeName = request.EmployeeName,
					Item = $"#{request.ItemId} {WarehouseRepository.GetItemNameByItemId(connection, request.ItemId)}",
					Unit = WarehouseRepository.GetUnitNameByItemId(connection, request.ItemId),
					Quantity = request.Quantity,
					TotalPriceNoVat = request.PriceNoVat,
					CommentEmployee = request.CommentEmployee,
					CommentCoordinator = request.CommentCoordinator,
					Status = WarehouseRepository.GetRequestStatusNameByRequestStatusId(connection, request.StatusId)
				});
			}

			WarehouseRepository.GetRequestStatusNames(connection, RequestStatusNames);
		}
	}
}
