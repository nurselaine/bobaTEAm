using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using ContosoCrafts.WebSite.Models;
using System.Linq;

namespace ContosoCrafts.WebSite.Services
{
	public class BobaUserService
	{
        private readonly string filePath;
        private string bobaUserFileName;

        public BobaUserService(IWebHostEnvironment webHostEnvironment)
        {
            bobaUserFileName = Path.Combine(webHostEnvironment.WebRootPath, "data", "bobausers.json");
        }

        public List<BobaUser> LoadUsers()
        {
            if (!File.Exists(bobaUserFileName))
                return new List<BobaUser>();

            string json = File.ReadAllText(bobaUserFileName);
            return JsonSerializer.Deserialize<List<BobaUser>>(json) ?? new List<BobaUser>();
        }

        public BobaUser CreateData(BobaUser user)
        {
            List<BobaUser> UserList = LoadUsers();
            UserList.Add(user);

            // save new data in Json file
            SaveData(UserList);

            return user;
        }

        public void SaveData(List<BobaUser> bobaUsers)
        {
            using var outputStream = File.OpenWrite(bobaUserFileName);

            JsonSerializer.Serialize<IEnumerable<BobaUser>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true
                }),
                bobaUsers
            );
        }
    }
}
