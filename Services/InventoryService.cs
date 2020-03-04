using System.Collections.Generic;
using LiteDB;
using Microsoft.Extensions.Logging;
using MyStore.Models;

namespace MyStore.Services
{
	public class InventoryService
	{
		ILiteCollection<InventoryItem> InventoryItems { get; set; }
		readonly ILogger<InventoryService> _logger;
		public InventoryService(ILogger<InventoryService> logger, LiteDatabase database, InventoryFixedDataService fixedService)
		{
			_logger = logger;
			InventoryItems = database.GetCollection<InventoryItem>("inventoryItem");
		}

		#region LiteDb Data
		public IEnumerable<InventoryItem> GetInventory() => InventoryItems.Query().ToEnumerable();

		public int InsertInventory(InventoryItem item) => InventoryItems.Insert(item).AsInt32;

		public bool DeleteInventory(int id) => InventoryItems.Delete(id);
		#endregion
	}
}
