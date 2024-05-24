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

        /// Find the data record
        /// Update the fields
        /// Save to the data store
        public bool UpdateData(MenuItem data)
        {
            bool isValidUpdate = false;

            var products = GetMenuItems();
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));
            if (productData == null)
            {
                return isValidUpdate;
            }

            // Create a new product list without the changed object
            var newProductList = products.Where(x => x.Id != data.Id);
            newProductList = newProductList.Append(data);

            // Store it back in Json File
            SaveData(newProductList);

            isValidUpdate = true;
            return isValidUpdate;
        }

        /// Create a new product using default values
        /// After create the user can update to set values
        public MenuItem CreateData(MenuItem data)
        {

            // Get the current set, and append the new record to it
            var dataSet = GetMenuItems();
            dataSet = dataSet.Append(data);

            SaveData(dataSet);

            // return the data
            return data;

        }


        public bool DeleteData(string productId)
        {
            bool isValidDelete = false;

            var products = GetMenuItems();
            var productData = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (productData == null)
            {
                return isValidDelete;
            }

            // Create a new product list without the changed object
            var newProductList = products.Where(x => x.Id != productId);

            // Store it back in Json File
            SaveData(newProductList);

            isValidDelete = true;
            return isValidDelete;
        }


        /// Save All products data to storage
        private void SaveData(IEnumerable<MenuItem> menuitems)
        {

            using (var outputStream = File.Create(menuFileName))
            {
                JsonSerializer.Serialize<IEnumerable<MenuItem>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    menuitems
                );
            }
        }



    }
}
