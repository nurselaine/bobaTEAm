using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ContosoCrafts.WebSite.Services
{
    public class JsonFileMenuServices
    {

		public IWebHostEnvironment WebHostEnvironment { get; }
        private string menuFileName;
        private string categoryFileName;

		public JsonFileMenuServices(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
			menuFileName = Path.Combine(WebHostEnvironment.WebRootPath, "data", "menu.json");
			categoryFileName = Path.Combine(WebHostEnvironment.WebRootPath, "data", "categories.json");

        
		}

     
        public IEnumerable<MenuItem> GetMenuItems()
        {
            using var jsonFileReader = File.OpenText(menuFileName);

            // deserialize JSON data into MenuData object
            return JsonSerializer.Deserialize<MenuItem[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
        }

        public IEnumerable<ProductCategory> GetCategories()
        {
			using var jsonFileReader = File.OpenText(categoryFileName);

			// deserialize JSON data into MenuData object
			return JsonSerializer.Deserialize<ProductCategory[]>(jsonFileReader.ReadToEnd(),
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
				});
		}

		public void AddRating(string productId, int rating)
		{
			var products = GetMenuItems().ToList();
			MenuItem selectedProductRatings = products.First(x => x.Name == productId);

			if (selectedProductRatings.Ratings == null)
			{
				selectedProductRatings.Ratings = new int[] { rating };
			}
			else
			{
				var ratings = products.First(x => x.Name == productId).Ratings.ToList();
				ratings.Add(rating);
				products.First(x => x.Name == productId).Ratings = ratings.ToArray();
			}

			using var outputStream = File.OpenWrite(menuFileName);

			JsonSerializer.Serialize<IEnumerable<MenuItem>>(
				new Utf8JsonWriter(outputStream, new JsonWriterOptions
				{
					SkipValidation = true,
					Indented = true
				}),
				products
			);
		}

	}
}
