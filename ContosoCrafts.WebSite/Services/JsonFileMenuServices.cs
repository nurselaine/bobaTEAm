using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
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

    }
}
