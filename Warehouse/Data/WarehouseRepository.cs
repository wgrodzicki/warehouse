using Warehouse.Models;
using Microsoft.Data.Sqlite;

namespace Warehouse.Data;

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
               VALUES('{item.Name}', {item.ItemGroupId}, {item.UnitId}, {item.Quantity},
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
    /// Pre-populates the 'item_groups' table with sample data.
    /// </summary>
    /// <param name="connection"></param>
    public static void PopulateItemGroups(SqliteConnection connection)
    {
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"INSERT INTO item_groups (name)
               VALUES('Group 1'), ('Group 2'), ('Group 3'), ('Group 4'), ('Group 5');";
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
			if (!String.IsNullOrEmpty(reader["name"].ToString()))
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
	public static string GetItemGroupNameById(SqliteConnection connection, int itemGroupId)
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
            if (!String.IsNullOrEmpty(reader["name"].ToString()))
            {
				itemGroupName = reader["name"].ToString();
				break;
			}
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
    public static int GetItemGroupIdByName(SqliteConnection connection, string itemGroupName)
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
            if (!String.IsNullOrEmpty(reader["id"].ToString()))
            {
				id = int.Parse(reader["id"].ToString());
				break;
			}
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
               VALUES('Unit 1'), ('Unit 2'), ('Unit 3'), ('Unit 4'), ('Unit 5');";
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
			if (!String.IsNullOrEmpty(reader["name"].ToString()))
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
	public static string GetUnitNameById(SqliteConnection connection, int unitId)
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
			if (!String.IsNullOrEmpty(reader["name"].ToString()))
			{
				unitName = reader["name"].ToString();
				break;
			}
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
	public static int GetUnitIdByName(SqliteConnection connection, string unitName)
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
			if (!String.IsNullOrEmpty(reader["id"].ToString()))
			{
				id = int.Parse(reader["id"].ToString());
				break;
			}
		}
		reader.Close();
		return id;
	}
}
