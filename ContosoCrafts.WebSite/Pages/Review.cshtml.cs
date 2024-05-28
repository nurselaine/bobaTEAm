using System;
using System.Collections.Generic;
using System.Linq;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages
{
    public class ReviewModel : PageModel
    {
        public ReviewModel(JsonFileMenuServices menuService, JsonFileReviewService reviewService)
        {
            MenuService = menuService;
			ReviewService = reviewService;
		}

        public JsonFileMenuServices MenuService { get; }
		public BobaUserService UserService { get; }

		public JsonFileReviewService ReviewService { get; }


		[BindProperty]
		public MenuItem Item { get; set; }
		[BindProperty]
		public IEnumerable<Review> Reviews { get; set; }
		[BindProperty]
		public Review review { get; set; }

		/// REST Get request
		/// Loads the Data
		public void OnGet(string id)
		{
			Item = MenuService.GetMenuItems().FirstOrDefault(m => m.Id.Equals(id));
			if (Item == null)
			{
				this.ModelState.AddModelError("OnGet Review (Item)", "Unable to pull up item info, Item is null");
			}

			Reviews = ReviewService.GetReviewItems().Where(x => x.ProductId.Equals(id));
			if (Reviews == null)
			{
				this.ModelState.AddModelError("OnGet Review (reviews)", "Unable to pull up reviews, reviews are null");
			}

			review = new Review()
			{
				Id = System.Guid.NewGuid().ToString(),
				Username = "",
				Title = "",
				Description = "",
				ProductId = "",
				UserId = "",
				Rating = 0,
				CreateDate = DateTime.MinValue
			};
			
		}

		/// Post the model back to the page
		/// The model is in the class variable Product
		/// Call the data layer to Update that data
		/// Then return to the index page
		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			review.CreateDate = DateTime.Now;
			ReviewService.CreateReview(review);
			MenuService.AddRating(review.ProductId, review.Rating);

			return RedirectToPage("./Index");
		}
	}
}
