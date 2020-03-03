using System.Collections.Generic;
using System.Linq;
using LiteDB;
using MyStore.Models;

namespace MyStore.Services
{
	public class InventoryService
	{
		private List<InventoryItem> fixedData = new List<InventoryItem> {
			new InventoryItem(1, "#2 pencil", "Pencil", .50, "38830982031", "A1", 100),
			new InventoryItem(2, "spiral notebook", "Notebook", 1.50, "3881111131", "A2", 50),
			new InventoryItem(3, "3 ring binder with dividers", "Binder", 4.50, "54830982031", "A2", 5),
			new InventoryItem(4, "Scientific calculator", "Ti83+ Calculator", 49.00, "3889462031", "A4", 100),
			new InventoryItem(5, "black ball point pen", "Pen", .50, "388309867", "A1", 10),
			new InventoryItem(6, "metallic coaster", "Coaster", 1.50, "388309212", "A6", 1),
			new InventoryItem(7, "Fuzzy backpack", "Backpack", 24.50, "388309987", "A5", 100)
		};
		ILiteCollection<InventoryItem> InventoryItems { get; set; }
		public InventoryService(LiteDatabase database)
		{
			InventoryItems = database.GetCollection<InventoryItem>("inventoryItem");
		}

		public List<InventoryItem> GetInventoryFixed()
		{
			return fixedData;
		}

		public int InsertInventoryFixed(InventoryItem item)
		{
			var lastId = fixedData.AsEnumerable().OrderByDescending(x => x.Id).First().Id;
			item.Id = lastId + 1;
			fixedData.Add(item);
			return item.Id;
		}

		public bool DeleteInventoryFixed(int id)
		{
			var index = fixedData.FindIndex(x => x.Id == id);
			if (index >= 0)
			{
				fixedData.RemoveAt(index);
				return true;
			}
			else
				return false;
		}





		public IEnumerable<InventoryItem> GetInventory()
		{
			return InventoryItems.Query().ToEnumerable();
		}

		public int InsertInventory(InventoryItem item)
		{
			var id = InventoryItems.Insert(item);
			return id.AsInt32;
		}

		public bool DeleteInventory(int id)
		{
			return InventoryItems.Delete(id);
		}
	}
}
