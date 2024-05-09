using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class MenuItem
    {

        public string? Name { get; set; }
        public string? Description { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        public string? Price { get; set; }
        public string? Category {  get; set; }
		public int[]? Ratings { get; set; }

		public override string ToString()
        {
            return JsonSerializer.Serialize<MenuItem>(this);
        }
    }

    public class ProductCategory
    {
        public string? CategoryName { get; set; }
        public List<MenuItem>? CategoryItems { get; set; }
    }
}
