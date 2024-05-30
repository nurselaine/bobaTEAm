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
			if (!File.Exists(orderFilename))
			{
				return new List<Order>();
			}

			using var jsonFileReader = File.OpenText(orderFilename);
			var json = jsonFileReader.ReadToEnd();

			// deserialize JSON data into OrderData object
			return JsonSerializer.Deserialize<IEnumerable<Order>>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			}) ?? new List<Order>();
		}

        public void ProcessOrder(string userId, Order order)
        {
            var orders = GetAllOrders().ToList();
            var existingOrder = orders.FirstOrDefault(o => o.Item.Name == order.Item.Name);

            if (existingOrder != null)
            {
                existingOrder.Quantity = order.Quantity;
            }
            else
            {
                orders.Add(order);
            }

            using var outputStream = File.Create(orderFilename);
            var writerOptions = new JsonWriterOptions
            {
                SkipValidation = true,
                Indented = true,
            };
            using var writer = new Utf8JsonWriter(outputStream, writerOptions);
            JsonSerializer.Serialize(writer, orders);
        }
    }
}
