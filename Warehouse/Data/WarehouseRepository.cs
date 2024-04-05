using Warehouse.Models;
using Microsoft.Data.Sqlite;

namespace Warehouse.Data;

public static class WarehouseRepository
{
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
                ItemGroup = reader["item_group"].ToString(),
                Unit = reader["unit"].ToString(),
                Quantity = int.Parse(reader["quantity"].ToString()),
                PriceNoVat = decimal.Parse(reader["price_no_vat"].ToString()),
                Status = reader["status"].ToString(),
                StorageLocation = reader["storage_location"].ToString(),
                ContactPerson = reader["contact_person"].ToString()
            });
        }
        reader.Close();
    }
}
