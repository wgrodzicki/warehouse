using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Warehouse.Models;
using Warehouse.Data;
using static Warehouse.Helpers.Helpers;

namespace Warehouse.Pages;

public class RequestsModel : PageModel
{
	[BindProperty] public List<Helpers.Helpers.RequestToDisplay> RequestsToDisplay { get; set; }
	[BindProperty] public int RequestIdToSearchFor { get; set; }
	[BindProperty] public string SortingMode { get; set; }

	public string AutoOpenRequestModal { get; set; }
	public int TableRowCounter { get; set; } = 0;

	private IConfiguration _configuration;

	public RequestsModel(IConfiguration configuration)
	{
		_configuration = configuration;

		RequestsToDisplay = new List<Helpers.Helpers.RequestToDisplay>();
		AutoOpenRequestModal = "no";
	}

	public IActionResult OnGet()
    {
		PopulateRequests();
		return Page();
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
				Item = WarehouseRepository.GetItemNameByItemId(connection, request.ItemId),
				Unit = WarehouseRepository.GetUnitNameByItemId(connection, request.ItemId),
				Quantity = request.Quantity,
				TotalPriceNoVat = request.PriceNoVat,
				CommentEmployee = request.CommentEmployee,
				CommentCoordinator = request.CommentCoordinator,
				Status = WarehouseRepository.GetRequestStatusNameByRequestStatusId(connection, request.StatusId)
			});
		}

		if (RequestsToDisplay[0].Id <= 0)
			RequestsToDisplay.Clear();

		return Page();
	}

	public IActionResult OnPostSortId()
	{
		PopulateRequests();

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.Id).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.Id).ToList();
		return Page();
	}

	public IActionResult OnPostSortEmployeeName()
	{
		PopulateRequests();

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.EmployeeName).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.EmployeeName).ToList();
		return Page();
	}

	public IActionResult OnPostSortItem()
	{
		PopulateRequests();

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.Item).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.Item).ToList();
		return Page();
	}

	public IActionResult OnPostSortUnit()
	{
		PopulateRequests();

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.Unit).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.Unit).ToList();
		return Page();
	}

	public IActionResult OnPostSortQuantity()
	{
		PopulateRequests();

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.Quantity).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.Quantity).ToList();
		return Page();
	}

	public IActionResult OnPostSortPrice()
	{
		PopulateRequests();

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.TotalPriceNoVat).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.TotalPriceNoVat).ToList();
		return Page();
	}

	public IActionResult OnPostSortCommentEmployee()
	{
		PopulateRequests();

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.CommentEmployee).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.CommentEmployee).ToList();
		return Page();
	}

	public IActionResult OnPostSortCommentCoordinator()
	{
		PopulateRequests();

		if (SortingMode == "↑")
			RequestsToDisplay = RequestsToDisplay.OrderBy(x => x.CommentCoordinator).ToList();
		else
			RequestsToDisplay = RequestsToDisplay.OrderByDescending(x => x.CommentCoordinator).ToList();
		return Page();
	}

	public IActionResult OnPostSortStatus()
	{
		PopulateRequests();

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
				RequestsToDisplay.Add(new Helpers.Helpers.RequestToDisplay
				{
					Id = request.Id,
					EmployeeName = request.EmployeeName,
					Item = WarehouseRepository.GetItemNameByItemId(connection, request.ItemId),
					Unit = WarehouseRepository.GetUnitNameByItemId(connection, request.ItemId),
					Quantity = request.Quantity,
					TotalPriceNoVat = request.PriceNoVat,
					CommentEmployee = request.CommentEmployee,
					CommentCoordinator = request.CommentCoordinator,
					Status = WarehouseRepository.GetRequestStatusNameByRequestStatusId(connection, request.StatusId)
				});
			}
		}
	}
}
