using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ContosoCrafts.WebSite.Services
{
	public class JsonFileOrderService
	{
		public IWebHostEnvironment WebHostEnvironment { get; }
		private string orderFilename;

		public JsonFileOrderService(IWebHostEnvironment webHostEnvironment)
		{
			WebHostEnvironment = webHostEnvironment;
			orderFilename = Path.Combine(WebHostEnvironment.WebRootPath, "data", "order.json");
		}

		public IEnumerable<Order> GetAllOrders()
		{
			using var jsonFileReader = File.OpenText(orderFilename);

			// deserialize JSON data into OrderData object
			return JsonSerializer.Deserialize<Order[]>(jsonFileReader.ReadToEnd(),
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
				});
		}

		public void ProcessOrder(string userId, Order order)
		{
	/*		var orders = GetAllOrders().ToList();

			orders.Add(order);

			using var outputStream = File.OpenWrite(orderFilename);

			JsonSerializer.Serialize<IEnumerable<MenuItem>>(
				new Utf8JsonWriter(outputStream, new JsonWriterOptions
				{
					SkipValidation = true,
					Indented = true
				}),
				orders
			);*/
		}
	}
}
