using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ContosoCrafts.WebSite.Services
{
	public class JsonFileReviewService
	{

		public IWebHostEnvironment WebHostEnvironment { get; }
		private string reviewFileName;

		public JsonFileReviewService(IWebHostEnvironment webHostEnvironment)
		{
			WebHostEnvironment = webHostEnvironment;
			reviewFileName = Path.Combine(WebHostEnvironment.WebRootPath, "data", "review.json");
		}

		public IEnumerable<Review> GetReviewItems()
		{
			using var jsonFileReader = File.OpenText(reviewFileName);

			// deserialize JSON data into MenuData object
			return JsonSerializer.Deserialize<Review[]>(jsonFileReader.ReadToEnd(),
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
				});
		}

		public Review CreateReview(Review review)
		{

			// Get the current set, and append the new record to it
			var reviews = GetReviewItems();
			reviews = reviews.Append(review);

			SaveReview(reviews);

			// return the data
			return review;

		}


		private void SaveReview(IEnumerable<Review> reviews)
		{

			using (var outputStream = File.Create(reviewFileName))
			{
				JsonSerializer.Serialize<IEnumerable<Review>>(
					new Utf8JsonWriter(outputStream, new JsonWriterOptions
					{
						SkipValidation = true,
						Indented = true
					}),
					reviews
				);
			}
		}

	}
}
