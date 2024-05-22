using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using ContosoCrafts.WebSite.Models;

namespace ContosoCrafts.WebSite.Services
{
	public class BobaUserService
	{
        private readonly string filePath;

        public BobaUserService(IWebHostEnvironment webHostEnvironment)
        {
            filePath = Path.Combine(webHostEnvironment.WebRootPath, "data", "bobausers.json");
        }

        public List<BobaUser> LoadUsers()
        {
            if (!File.Exists(filePath))
                return new List<BobaUser>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<BobaUser>>(json) ?? new List<BobaUser>();
        }

        public void SaveUsers(List<BobaUser> users)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(users, options);
            File.WriteAllText(filePath, json);
        }
    }
}
