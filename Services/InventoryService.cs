using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Microsoft.Extensions.Logging;
using MyStore.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MyStore.Services
{
	public class InventoryService
	{
		ILiteCollection<InventoryItem> InventoryItems { get; set; }
		readonly InventoryFixedDataService _fixedDataService;
		readonly ILogger<InventoryService> _logger;
		public InventoryService(ILogger<InventoryService> logger, LiteDatabase database, InventoryFixedDataService fixedService)
		{
			_logger = logger;
			InventoryItems = database.GetCollection<InventoryItem>("inventoryItem");
			_fixedDataService = fixedService;
		}

		#region Fixed Data
		public List<InventoryItem> GetInventoryFixed() => _fixedDataService.fixedData;

		public int InsertInventoryFixed(InventoryItem item)
		{
			_logger.LogInformation(JsonSerializer.Serialize(item));
			var lastId = _fixedDataService.fixedData.AsEnumerable().OrderByDescending(x => x.Id).First().Id;
			item.Id = lastId + 1;
			_fixedDataService.fixedData.Add(item);
			return item.Id;
		}

		public bool DeleteInventoryFixed(int id)
		{
			var index = _fixedDataService.fixedData.FindIndex(x => x.Id == id);
			if (index >= 0)
			{
				_fixedDataService.fixedData.RemoveAt(index);
				return true;
			}
			else
				return false;
		}
		#endregion

		#region LiteDb Data
		public IEnumerable<InventoryItem> GetInventory() => InventoryItems.Query().ToEnumerable();

		public int InsertInventory(InventoryItem item) => InventoryItems.Insert(item).AsInt32;

		public bool DeleteInventory(int id) => InventoryItems.Delete(id);
		#endregion
	}

	public class InventoryFixedDataService
	{
		public List<InventoryItem> fixedData = new List<InventoryItem> {
			new InventoryItem(1, "#2 pencil", "Pencil", .50, "38830982031", "A1", 100),
			new InventoryItem(2, "spiral notebook", "Notebook", 1.50, "3881111131", "A2", 50),
			new InventoryItem(3, "3 ring binder with dividers", "Binder", 4.50, "54830982031", "A2", 5),
			new InventoryItem(4, "Scientific calculator", "Ti83+ Calculator", 49.00, "3889462031", "A4", 100),
			new InventoryItem(5, "black ball point pen", "Pen", .50, "388309867", "A1", 10),
			new InventoryItem(6, "metallic coaster", "Coaster", 1.50, "388309212", "A6", 1),
			new InventoryItem(7, "Fuzzy backpack", "Backpack", 24.50, "388309987", "A5", 100)
		};
	}
}
