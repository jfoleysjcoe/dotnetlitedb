using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Services;

namespace MyStore.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class InventoryController : ControllerBase
	{
		private readonly InventoryService _service;
		private readonly InventoryFixedDataService _fixedService;

		public InventoryController(InventoryService inventory, InventoryFixedDataService fixedDataService)
		{
			_service = inventory;
			_fixedService = fixedDataService;
		}

		[HttpGet()]
		public ActionResult<IEnumerable<InventoryItem>> GetItems() => Ok(_fixedService.fixedData);

		[HttpPost("insert")]
		public ActionResult<int> InsertInventoryItem(InventoryItem item) => Ok(_fixedService.Insert(item));

		[HttpDelete("{id}")]
		public ActionResult<bool> DeleteInventoryItem(int id) => Ok(_fixedService.Delete(id));

		[HttpPost("update")]
		public InventoryItem Update(InventoryItem item) => _fixedService.Update(item);

		[HttpGet("findBelowPrice/{price}")]
		public IEnumerable<InventoryItem> FindBelowPrice(double price) => _fixedService.GetItemsLessThan(price);

	}
}
