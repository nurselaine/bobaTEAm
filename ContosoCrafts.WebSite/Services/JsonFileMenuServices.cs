using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ContosoCrafts.WebSite.Services
{
    public class JsonFileMenuServices
    {

		public IWebHostEnvironment WebHostEnvironment { get; }
        private string JsonFileName;

		public JsonFileMenuServices(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
            JsonFileName = Path.Combine(WebHostEnvironment.WebRootPath, "data", "menu.json"); 
		}

     
        public IEnumerable<MenuItem> GetMenuItems()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);

            // deserialize JSON data into MenuData object
            return JsonSerializer.Deserialize<MenuItem[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
        }


    }
}
