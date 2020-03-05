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
		readonly InventoryFixedDataService _fixedService;
		readonly InventoryService _service;

		public InventoryController(
			InventoryFixedDataService fixedService,
			InventoryService service
		)
		{
			_fixedService = fixedService;
			_service = service;
		}

		[HttpGet("populate")]
		public int PopulateData()
		{
			var fixedData = _fixedService.fixedData;
			return _service.PopulateData(fixedData);
		}

		[HttpGet]
		public IEnumerable<InventoryItem> GetInventoryItems()
		{
			// return _fixedService.fixedData;
			return _service.getInventoryItems();
		}

		[HttpGet("{id}")]
		public InventoryItem GetInventoryItem(int id)
		{
			// return _fixedService.fixedData.AsEnumerable().First(x => x.Id == id);
			return _service.GetInventoryItemById(id);
		}

		[HttpPost]
		public int AddInventoryItem(InventoryItem item)
		{
			// return _fixedService.Insert(item);
			return _service.Insert(item);
		}

		[HttpDelete("{id}")]
		public bool DeleteInventoryItem(int id)
		{
			//return _fixedService.Delete(id);
			return _service.Delete(id);
		}

		[HttpPost("update")]
		public InventoryItem Update(InventoryItem item)
		{
			//return _fixedService.Update(item);
			return _service.Update(item);
		}

		[HttpGet("findBelowPrice/{price}")]
		public IEnumerable<InventoryItem> FindBelowPrice(double price)
		{
			// return _fixedService.GetItemsLessThan(price);
			return _service.GetItemsLessThan(price);
		}

		[HttpPost("update/name")]
		public bool UpdateName(ChangeNameRequest request)
		{
			return _service.UpdateName(request);
		}

		[HttpGet("ItemsInLocation/{location}")]
		public IEnumerable<ChangeNameRequest> GetNameAndIdInLocation(string location)
		{
			return _service.GetNameAndIdsInStorageLocation(location);
		}
	}
}
