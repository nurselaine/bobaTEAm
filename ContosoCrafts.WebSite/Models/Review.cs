using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
	public class Review
	{
		public string? Id { get; set; }

		public string? Username { get; set; }

		public string? Title { get; set; }

		public string? Description { get; set; }

		public string? ProductId { get; set; }
		public string? UserId { get; set; }

		public int Rating { get; set; }

		public DateTime? CreateDate { get; set; }

		public override string ToString()
		{
			return JsonSerializer.Serialize<Review>(this);
		}
	}

}
