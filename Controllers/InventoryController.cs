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

		public InventoryController(InventoryService inventory)
		{
			_service = inventory;
		}

		[HttpGet()]
		public ActionResult<IEnumerable<InventoryItem>> GetItems() => Ok(_service.GetInventoryFixed());

		[HttpPost("insert")]
		public ActionResult<int> InsertInventoryItem(InventoryItem item) => Ok(_service.InsertInventoryFixed(item));

		[HttpDelete("{id}")]
		public ActionResult<bool> DeleteInventoryItem(int id) => Ok(_service.DeleteInventoryFixed(id));

	}
}
