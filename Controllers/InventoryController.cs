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
		public ActionResult<IEnumerable<InventoryItem>> GetItems() {
			return Ok(_service.GetInventoryFixed());
		}

		[HttpPost("insert")]
		public ActionResult<int> InsertInventoryItem(InventoryItem item) {
			return Ok(_service.InsertInventoryFixed(item));
		}

		[HttpDelete("{id}")]
		public ActionResult<bool> DeleteInventoryItem(int id) {
			return Ok(_service.DeleteInventoryFixed(id));
		}

	}
}
