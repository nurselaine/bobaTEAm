using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json");

        public IEnumerable<Product> GetProducts()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();

            if (products.First(x => x.Id == productId).Ratings == null)
            {
                products.First(x => x.Id == productId).Ratings = new int[] { rating };
            }
            else
            {
                var ratings = products.First(x => x.Id == productId).Ratings.ToList();
                ratings.Add(rating);
                products.First(x => x.Id == productId).Ratings = ratings.ToArray();
            }

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Product>>(
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
        public bool UpdateData(Product data)
        {
            bool isValidUpdate = false;

            var products = GetProducts();
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
        public Product CreateData(Product data)
        {

            // Get the current set, and append the new record to it
            var dataSet = GetProducts();
            dataSet = dataSet.Append(data);

            SaveData(dataSet);

            // return the data
            return data;

        }


        public bool DeleteData(string productId)
        {
            bool isValidDelete = false;

            var products = GetProducts();
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
        private void SaveData(IEnumerable<Product> products)
        {

            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
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
}
