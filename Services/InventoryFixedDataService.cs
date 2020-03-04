using System.Collections.Generic;
using System.Linq;
using MyStore.Models;

namespace MyStore.Services
{
	public class InventoryFixedDataService
	{
		public List<InventoryItem> fixedData = new List<InventoryItem> {
			new InventoryItem(1, "#2 pencil", "Pencil", .50, "38830982031", "A1", 100),
			new InventoryItem(2, "spiral notebook", "Notebook", 1.50, "3881111131", "A2", 50),
			new InventoryItem(3, "3 ring binder with dividers", "Binder", 4.50, "54830982031", "A2", 5),
			new InventoryItem(4, "Scientific calculator", "Ti83+ Calculator", 49.00, "3889462031", "A4", 100),
			new InventoryItem(5, "black ball point pen", "Pen", .50, "388309867", "A1", 10),
			new InventoryItem(6, "metallic coaster", "Coaster", 5.50, "388309212", "A6", 1),
			new InventoryItem(7, "Fuzzy backpack", "Backpack", 25.49, "388309987", "A5", 100)
		};
		public int Insert(InventoryItem item)
		{
			var lastId = fixedData.OrderByDescending(x => x.Id).First().Id;
			item.Id = lastId + 1;
			fixedData.Add(item);
			return item.Id;
		}

		public bool Delete(int id)
		{
			var index = fixedData.FindIndex(x => x.Id == id);
			if (index > -1)
			{
				fixedData.RemoveAt(index);
				return true;
			}
			return false;
		}

		public InventoryItem Update(InventoryItem item)
		{
			var dataItem = fixedData.Find(x => x.Id == item.Id);
			dataItem.Description = item.Description;
			dataItem.IsSaleItem = item.IsSaleItem;
			dataItem.Price = item.Price;
			dataItem.Name = item.Name;
			dataItem.Sku = item.Sku;
			dataItem.StorageLocation = item.StorageLocation;

			return dataItem;
		}

		public IEnumerable<InventoryItem> GetItemsLessThan(double price)
		{
			return fixedData.Where(x => x.Price < price);
		}
	}
}
