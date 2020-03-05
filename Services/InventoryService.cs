using System.Collections.Generic;
using System.Linq;
using LiteDB;
using MyStore.Models;

namespace MyStore.Services
{
	public class InventoryService
	{
		ILiteCollection<InventoryItem> Inventory;
		public InventoryService(LiteDatabase database)
		{
			Inventory = database.GetCollection<InventoryItem>("inventoryItem");
		}
		public int PopulateData(List<InventoryItem> items)
		{
			return Inventory.InsertBulk(items);
		}
		public IEnumerable<InventoryItem> getInventoryItems()
		{
			return Inventory.Query().ToEnumerable();
		}

		public InventoryItem Update(InventoryItem item)
		{
			Inventory.Update(item);
			return item;
		}

		public bool Delete(int id)
		{
			return Inventory.Delete(id);
		}
		public InventoryItem GetInventoryItemById(int id)
		{
			return Inventory.FindById(id);
		}

		public int Insert(InventoryItem item)
		{
			return Inventory.Insert(item);
		}

		public bool UpdateName(ChangeNameRequest request)
		{
			var item = Inventory.FindById(request.Id);
			item.Name = request.Name;
			return Inventory.Update(item);
		}

		public IEnumerable<InventoryItem> GetItemsLessThan(double price)
		{
			// return Inventory.Find(x => x.Price < price);
			return Inventory.Query()
				.Where(x => x.Price < price)
				.ToEnumerable();
		}

		public IEnumerable<ChangeNameRequest> GetNameAndIdsInStorageLocation(string location)
		{
			var items = Inventory.Find(x => x.StorageLocation == location);

			var requests = new List<ChangeNameRequest>();
			for (int i = 0; i < items.Count(); i++)
			{
				var n = new ChangeNameRequest()
				{
					Name = items.ElementAt(i).Name,
					Id = items.ElementAt(i).Id
				};
				requests.Add(n);
			}
			return items.Select(x =>
			{
				return new ChangeNameRequest
				{
					Id = x.Id,
					Name = x.Name
				};
			});
		}
	}
}
