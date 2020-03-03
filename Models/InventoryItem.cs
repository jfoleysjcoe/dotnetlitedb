using LiteDB;

namespace MyStore.Models
{
	public class InventoryItem
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public string Sku { get; set; }
		public string StorageLocation { get; set; }
		public int Quantity { get; set; }

		public bool IsSaleItem { get; set; }
		[BsonIgnore] public bool HasDescription => Description != null && Description != "";

		public InventoryItem() { }

		public InventoryItem(int id, string description, string name, double price, string sku, string storageLocation, int quantity)
		{
			this.Id = id;
			this.Description = description;
			this.Name = name;
			this.Price = price;
			this.Sku = sku;
			this.StorageLocation = storageLocation;
			this.Quantity = quantity;
			this.IsSaleItem = false;
		}
	}
}
