using Warehouse.Models;
using Microsoft.Data.Sqlite;

namespace Warehouse.Data;

/// <summary>
/// Provides methods to manipulate tables in the Warehouse database.
/// </summary>
public static class WarehouseRepository
{
    /// <summary>
    /// Adds new item to the 'items' table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="item"></param>
    public static void AddItem(SqliteConnection connection, ItemModel item)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            @$"INSERT INTO items (name, item_group_id, unit_id, quantity, price_no_vat, status, storage_location, contact_person)
               VALUES ('{item.Name}', {item.ItemGroupId}, {item.UnitId}, {item.Quantity},
                       {item.PriceNoVat}, '{item.Status}', '{item.StorageLocation}', '{item.ContactPerson}');";
		tableCmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Retrieves all items from the 'items' table.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="items"></param>
	public static void GetAllItems(SqliteConnection connection, List<ItemModel> items)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            @"SELECT * FROM items;";
        tableCmd.CommandType = System.Data.CommandType.Text;

        SqliteDataReader reader = tableCmd.ExecuteReader();
        while (reader.Read())
        {
            items.Add(new ItemModel
            {
                Id = int.Parse(reader["id"].ToString()),
                Name = reader["name"].ToString(),
                ItemGroupId = int.Parse(reader["item_group_id"].ToString()),
                UnitId = int.Parse(reader["unit_id"].ToString()),
                Quantity = int.Parse(reader["quantity"].ToString()),
                PriceNoVat = decimal.Parse(reader["price_no_vat"].ToString()),
                Status = reader["status"].ToString(),
                StorageLocation = reader["storage_location"].ToString(),
                ContactPerson = reader["contact_person"].ToString()
            });
        }
        reader.Close();
    }

	/// <summary>
	/// Retrieves all items from the 'items' table whose name matches the given name.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="itemName"></param>
	/// <param name="items"></param>
	public static void GetItemsByItemName(SqliteConnection connection, string itemName, List<ItemModel> items)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@$"SELECT * FROM items
			   WHERE name LIKE '%{itemName}%';";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			items.Add(new ItemModel
			{
				Id = int.Parse(reader["id"].ToString()),
				Name = reader["name"].ToString(),
				ItemGroupId = int.Parse(reader["item_group_id"].ToString()),
				UnitId = int.Parse(reader["unit_id"].ToString()),
				Quantity = int.Parse(reader["quantity"].ToString()),
				PriceNoVat = decimal.Parse(reader["price_no_vat"].ToString()),
				Status = reader["status"].ToString(),
				StorageLocation = reader["storage_location"].ToString(),
				ContactPerson = reader["contact_person"].ToString()
			});
		}
		reader.Close();
	}

	/// <summary>
	/// Retrieves item from the 'items' table based on item id.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public static ItemModel GetItemByItemId(SqliteConnection connection, int itemId)
	{
		ItemModel item = new ItemModel();

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"SELECT * FROM items
			   WHERE id = {itemId};";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			item.Id = int.Parse(reader["id"].ToString());
			item.Name = reader["name"].ToString();
			item.ItemGroupId = int.Parse(reader["item_group_id"].ToString());
			item.UnitId = int.Parse(reader["unit_id"].ToString());
			item.Quantity = int.Parse(reader["quantity"].ToString());
			item.PriceNoVat = decimal.Parse(reader["price_no_vat"].ToString());
			item.Status = reader["status"].ToString();
			item.StorageLocation = reader["storage_location"].ToString();
			item.ContactPerson = reader["contact_person"].ToString();
		}
		reader.Close();
		return item;
	}

	/// <summary>
	/// Retrieves item name from the 'items' table based on item id.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public static string GetItemNameByItemId(SqliteConnection connection, int itemId)
	{
		string itemName = "";

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@$"SELECT name FROM items
               WHERE id = {itemId};";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			itemName = reader["name"].ToString();
		}
		reader.Close();
		return itemName;
	}

	/// <summary>
	/// Retrieves item id from the 'items' table based on item name.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="itemName"></param>
	/// <returns></returns>
	public static int GetItemIdByItemName(SqliteConnection connection, string itemName)
	{
		int id = -1;

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@$"SELECT id FROM items
               WHERE name = '{itemName}';";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			id = int.Parse(reader["id"].ToString());
		}
		reader.Close();
		return id;
	}

	/// <summary>
	/// Retrieves item price from the 'items' table based on item id.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public static decimal GetItemPriceByItemId(SqliteConnection connection, int itemId)
	{
		decimal price = 0;

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"SELECT price_no_vat FROM items
			   WHERE id = {itemId};";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			price = decimal.Parse(reader["price_no_vat"].ToString());
		}
		reader.Close();
		return price;
	}

	/// <summary>
	/// Retrieves item group name from the 'items' table based on item id.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public static string GetItemGroupNameByItemId(SqliteConnection connection, int itemId)
	{
		string itemGroupName = "";

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@$"SELECT name FROM item_groups
               WHERE id = (
			       SELECT item_group_id FROM items
				   WHERE id = {itemId}
			   );";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			itemGroupName = reader["item_group_id"].ToString();
		}
		reader.Close();
		return itemGroupName;
	}

	/// <summary>
	/// Retrieves unit name from the 'items' table based on item id.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public static string GetUnitNameByItemId(SqliteConnection connection, int itemId)
	{
		string unitName = "";

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@$"SELECT name FROM units
               WHERE id = (
			       SELECT unit_id FROM items
				   WHERE id = {itemId}
			   );";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			unitName = reader["name"].ToString();
		}
		reader.Close();
		return unitName;
	}

	/// <summary>
	/// Deletes an item from the 'items' table based on its id.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="itemId"></param>
	public static void DeleteItem(SqliteConnection connection, int itemId)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"DELETE FROM items
			   WHERE id = {itemId};";
		tableCmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Updates item of the given id in the 'items' table with the supplied model data.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="item"></param>
	/// <param name="itemId"></param>
	public static void UpdateItem(SqliteConnection connection, ItemModel item, int itemId)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"UPDATE items
			   SET name = '{item.Name}',
				   item_group_id = {item.ItemGroupId},
				   unit_id = {item.UnitId},
				   quantity = {item.Quantity},
				   price_no_vat = {item.PriceNoVat},
				   status = '{item.Status}',
				   storage_location = '{item.StorageLocation}',
				   contact_person = '{item.ContactPerson}'
			   WHERE id = {itemId};";
		tableCmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Pre-populates the 'item_groups' table with sample data.
	/// </summary>
	/// <param name="connection"></param>
	public static void PopulateItemGroups(SqliteConnection connection)
    {
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"INSERT INTO item_groups (name)
               VALUES ('Group 1'), ('Group 2'), ('Group 3'), ('Group 4'), ('Group 5');";
		tableCmd.ExecuteNonQuery();
	}

    /// <summary>
    /// Retrieves all group names from the 'item_groups' table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="itemGroupNames"></param>
    public static void GetItemGroupNames(SqliteConnection connection, List<string> itemGroupNames)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            @"SELECT name FROM item_groups;";
        tableCmd.CommandType = System.Data.CommandType.Text;

        SqliteDataReader reader = tableCmd.ExecuteReader();
        while (reader.Read())
        {
			itemGroupNames.Add(reader["name"].ToString());
        }
        reader.Close();
    }

	/// <summary>
	/// Retrieves item group name from the 'item_groups' table based on item group id.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="itemGroupId"></param>
	/// <returns></returns>
	public static string GetItemGroupNameByItemGroupId(SqliteConnection connection, int itemGroupId)
    {
        string itemGroupName = "";

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@$"SELECT name FROM item_groups
               WHERE id = {itemGroupId};";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			itemGroupName = reader["name"].ToString();
		}
		reader.Close();
		return itemGroupName;
	}

    /// <summary>
    /// Retrieves item group id from the 'item_groups' table based on item group name.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="itemGroupName"></param>
    /// <returns></returns>
    public static int GetItemGroupIdByItemGroupName(SqliteConnection connection, string itemGroupName)
    {
        int id = -1;

        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            @$"SELECT id FROM item_groups
               WHERE name = '{itemGroupName}';";
        tableCmd.CommandType = System.Data.CommandType.Text;

        SqliteDataReader reader = tableCmd.ExecuteReader();
        while (reader.Read())
        {
			id = int.Parse(reader["id"].ToString());
        }
		reader.Close();
		return id;
    }

	/// <summary>
	/// Pre-populates the 'units' table with sample data.
	/// </summary>
	/// <param name="connection"></param>
	public static void PopulateUnits(SqliteConnection connection)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"INSERT INTO units (name)
               VALUES ('Unit 1'), ('Unit 2'), ('Unit 3'), ('Unit 4'), ('Unit 5');";
		tableCmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Retrieves all unit names from the 'units' table.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="unitNames"></param>
	public static void GetUnitNames(SqliteConnection connection, List<string> unitNames)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@"SELECT name FROM units;";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			unitNames.Add(reader["name"].ToString());
		}
		reader.Close();
	}

	/// <summary>
	/// Retrieves unit name from the 'units' table based on unit id.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="unitId"></param>
	/// <returns></returns>
	public static string GetUnitNameByUnitId(SqliteConnection connection, int unitId)
	{
		string unitName = "";

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@$"SELECT name FROM units
               WHERE id = {unitId};";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			unitName = reader["name"].ToString();
		}
		reader.Close();
		return unitName;
	}

	/// <summary>
	/// Retrieves unit id from the 'units' table based on unit name.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="unitName"></param>
	/// <returns></returns>
	public static int GetUnitIdByUnitName(SqliteConnection connection, string unitName)
	{
		int id = -1;

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@$"SELECT id FROM units
               WHERE name = '{unitName}';";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			id = int.Parse(reader["id"].ToString());
		}
		reader.Close();
		return id;
	}

	/// <summary>
	/// Adds new request to the 'requests' table.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="request"></param>
	public static void AddRequest(SqliteConnection connection, RequestModel request)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"INSERT INTO requests (item_id, employee_name, quantity, price_no_vat, comment_employee, comment_coordinator, status_id)
			   VALUES ({request.ItemId}, '{request.EmployeeName}', {request.Quantity}, {request.PriceNoVat}, '{request.CommentEmployee}',
					   '{request.CommentCoordinator}', {request.StatusId});";
		tableCmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Retrieves all requests from the 'requests' table.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="requests"></param>
	public static void GetAllRequests(SqliteConnection connection, List<RequestModel> requests)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@"SELECT * FROM requests;";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			requests.Add(new RequestModel
			{
				Id = int.Parse(reader["id"].ToString()),
				ItemId = int.Parse(reader["item_id"].ToString()),
				EmployeeName = reader["employee_name"].ToString(),
				Quantity = int.Parse(reader["quantity"].ToString()),
				PriceNoVat = decimal.Parse(reader["price_no_vat"].ToString()),
				CommentEmployee = reader["comment_employee"].ToString(),
				CommentCoordinator = reader["comment_coordinator"].ToString(),
				StatusId = int.Parse(reader["status_id"].ToString())
			});
		}
		reader.Close();
	}

	/// <summary>
	/// Retrieves request from the 'requests' table based on request id.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="requestId"></param>
	/// <param name="request"></param>
	public static void GetRequestByRequestId(SqliteConnection connection, int requestId, RequestModel request)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@$"SELECT * FROM requests
			   WHERE id = {requestId};";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			request.Id = int.Parse(reader["id"].ToString());
			request.ItemId = int.Parse(reader["item_id"].ToString());
			request.EmployeeName = reader["employee_name"].ToString();
			request.Quantity = int.Parse(reader["quantity"].ToString());
			request.PriceNoVat = decimal.Parse(reader["price_no_vat"].ToString());
			request.CommentEmployee = reader["comment_employee"].ToString();
			request.CommentCoordinator = reader["comment_coordinator"].ToString();
			request.StatusId = int.Parse(reader["status_id"].ToString());
		}
		reader.Close();
	}

	/// <summary>
	/// Updates request of the given id in the 'requests' table with the supplied model data.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="request"></param>
	/// <param name="requestId"></param>
	public static void UpdateRequest(SqliteConnection connection, RequestModel request, int requestId)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"UPDATE requests
			   SET item_id = {request.ItemId},
				   employee_name = '{request.EmployeeName}',
				   quantity = {request.Quantity},
				   price_no_vat = {request.PriceNoVat},
				   comment_employee = '{request.CommentEmployee}',
				   comment_coordinator = '{request.CommentCoordinator}',
				   status_id = {request.StatusId}
			   WHERE id = {requestId};";
		tableCmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Pre-populates the 'request_statuses' table with sample data.
	/// </summary>
	/// <param name="connection"></param>
	public static void PopulateRequestStatuses(SqliteConnection connection)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"INSERT INTO request_statuses (name)
               VALUES ('New'), ('Accepted'), ('Rejected');";
		tableCmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Retrieves all request status names from the 'request_statuses' table.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="requestStatusNames"></param>
	public static void GetRequestStatusNames(SqliteConnection connection, List<string> requestStatusNames)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@"SELECT name FROM request_statuses;";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			requestStatusNames.Add(reader["name"].ToString());
		}
		reader.Close();
	}

	/// <summary>
	/// Retrieves request status id from the 'request_statuses' table based on request status name.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="requestStatusName"></param>
	/// <returns></returns>
	public static int GetRequestStatusIdByRequestStatusName(SqliteConnection connection, string requestStatusName)
	{
		int id = -1;

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"SELECT id FROM request_statuses
			   WHERE name = '{requestStatusName}';";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			id = int.Parse(reader["id"].ToString());
		}
		reader.Close();
		return id;
	}

	/// <summary>
	/// Retrieves request status name from the 'request_statuses' table based on request status id.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="requestStatusId"></param>
	/// <returns></returns>
	public static string GetRequestStatusNameByRequestStatusId(SqliteConnection connection, int requestStatusId)
	{
		string name = "";

		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"SELECT name FROM request_statuses
			   WHERE id = {requestStatusId};";
		tableCmd.CommandType = System.Data.CommandType.Text;

		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			name = reader["name"].ToString();
		}
		reader.Close();
		return name;
	}
}
