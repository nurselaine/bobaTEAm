using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
	public class Review
	{
		public string? Id { get; set; }

		[Required]
		[StringLength(25, MinimumLength = 3,
			ErrorMessage = "{0} length must be between {2} and {1}." )]
		public string? Username { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 10,
			ErrorMessage = "{0} length must be between {2} and {1}.")]
		public string? Title { get; set; }

		[Required]
		[StringLength(500, MinimumLength = 10,
			ErrorMessage = "{0} length must be between {2} and {1}.")]
		public string? Description { get; set; }

		public string? ProductId { get; set; }
		public string? UserId { get; set; }

		[Required]
		[Range(1, 5)]
		public int Rating { get; set; }

		public DateTime? CreateDate { get; set; }

		public override string ToString()
		{
			return JsonSerializer.Serialize<Review>(this);
		}
	}

}
