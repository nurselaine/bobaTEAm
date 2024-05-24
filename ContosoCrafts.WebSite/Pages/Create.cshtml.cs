using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    /// Manage the Update of the data for a single record
    public class CreateModel : PageModel
    {
        // Data middletier
        public JsonFileMenuServices ProductService { get; }

        /// Default Construtor
        public CreateModel(JsonFileMenuServices productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public MenuItem Item { get; set; }

        /// REST Get request
        /// Loads the Data
        public void OnGet()
        {
            Item = new MenuItem()
            {
                Name = System.Guid.NewGuid().ToString(),
                Description = "",
                Image = "",
                Price = "",
                Category = "",
                Ratings = { }
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

            ProductService.CreateData(Item);

            return RedirectToPage("./DedicatedIndex");
        }
    }
}