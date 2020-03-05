# .Net Core API with LiteDB
Last Updated: 3/3/2020

## Ideologies

The most common project structures are

### Model/Controller/Service
The most commonly used.
```
/InventoryApi
	/Controllers
		InventoryController.cs
		UserController.cs
	/Models
		InventoryItem.cs
		User.cs
	/Services
		InventoryService.cs
		UserService.cs
	appsettings.json
	Program.cs
	Startup.cs
	InventoryApi.csproj
```

### Domain Driven

Separation by subject

```
/InventoryApi
	/Inventory
		/Models
			InventoryItem.cs
		Controller.cs
		Service.cs
	/User
		/Models
			User.cs
		Controller.cs
		Service.cs
	appsettings.json
	Program.cs
	Startup.cs
	InventoryApi.csproj
```

### Clean Architecture

The industry preference


	/InventoryApi
		/Controllers
			InventoryController.cs
			UserController.cs
		appsettings.json
		Program.cs
		Startup.cs
		InventoryApi.csproj


	/InventoryModels
		/Models
			InventoryItem.cs
			User.cs
		/Enums
			UserType.cs
		InventoryModels.csproj

	/InventoryServices
		/Services
			InventoryService.cs
			UserService.cs
		InventoryServices.csproj

	/InventoryDataAccess
		/Migrations
		InventoryDataAccess.csproj


## Creating the .Net Core API using .Net CLI.

`dotnet new webapi -o [name]`

in this example we created an inventory api

`dotnet new webapi -o InventoryApi`

### Add LiteDB to project
`dotnet add package LiteDB`

## Lecture Take-aways:

### Controllers

Identify scope and purpose

1. Determine Routes

	Keywords ([`controller`] [`action`])

	Routes must be unique and are quantified by Route Type (Get/Post) and Full Path

	Example:
	```C#
	// InventoryController.cs
	[ApiController]
	[Route("api/[controller]")]
	public class InventoryController : ControllerBase
	{
		[HttpGet()] // default route ".../api/inventory
		public IEnumerable<InventoryItem> Get() {...}

		[HttpGet("{id}")] // .../api/inventory/3
		public InventoryItem GetInventoryItem(int id) {...}

		[HttpPost("{id}")] // ... /api/inventory/3
		public int SaveInventoryItem(int id, InventoryItem item) { ... }

		// Note GetInventoryItem and SaveInventoryItem have save path but are still unique
		// because of the different route types (one is HttpGet and the other is HttpPost)

	}
	```

2. Controller Return types

	Most common are `IActionResult`, `JsonResult`, `ContentResult`.

	In our example api lecture, we simple returned the content being requests. Which was usually `IEnumerable<Type>`, `int` or `bool`.

### Services

Creating a service involves 3 parts.

1. Identify its purpose and context
2. Declare as a service in start up
	```C#
	// Startup.cs
	public void ConfigureServices(IServiceCollection services)
	{
		...
		services.AddScoped<InventoryService>();
		services.AddSingleton<InventoryFixedDataService>();
	}
	```

3. Inject into Controller (or other services)

	```C#
	// InventoryController.cs
	public class InventoryController : ControllerBase
	{
		private readonly InventoryService _service;

		public InventoryController(InventoryService inventoryService)
		{
			_service = inventoryService;
		}
	}
	```

## LiteDb
Examples and WikiDoc can be found here

https://github.com/mbdavid/LiteDB

http://www.litedb.org/docs/

### Install
`dotnet add package LiteDb`


### Set up

Create DB Singleton (since we only want one instance of the database running)

```C#
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
	services.AddScoped<InventoryController>();
	services.AddSingleton<LiteDatabase>(new LiteDatabase(@"Filename=./Data/LiteDb.db;Mode=Shared"));
	// you might need to have Mode=Exclusive, depends on your machine's permissions/settings.
}
```


### Create Model

```C#
public class InventoryItem
{
	public int Id { get; set; }
	public string Description { get; set; }
	public string Name { get; set; }
	public double Price { get; set; }
	public string Sku { get; set; }
	public string StorageLocation { get; set; }
	public int Quantity { get; set; }
	public int IsSaleItem { get; set; }
	[BsonIgnore] public bool HasDescription => Description != null && Description != "";
}
```
`[BsonIgnore]` will not add that property to the database (Specific to LiteDb)

`[JsonIgnore]` will ignore that property when transferring to/from client

### Updating the database

```C#
// insert
var newItem = new InventoryItem
{
	Description = "Wooden 36 inch desk, collapsable with small drawer",
	Name = "Folding Desk",
	Price = 24.99,
	Sku = "389284919383",
	StorageLocation = "A12",
	Quantity = 5,
	IsSaleItem = false
}

var inventory = db.GetCollection<InventoryItem>("inventoryItem");
// Insert new customer document (Id will be auto-incremented)
inventory.Insert(newItem);

// Update a document inside a collection
newItem.Price = 19.49;
newItem.IsSaleItem = true;

col.Update(newItem);

// Index document using document Name property
col.EnsureIndex(x => x.Name);

// Use LINQ to query documents (filter, sort, transform)
var results = col.Query()
		.Where(x => x.Name.StartsWith("F"))
		.OrderBy(x => x.Name)
		.Select(x => new { x.Name, NameUpper = x.Name.ToUpper() })
		.Limit(10)
		.ToList();
```
