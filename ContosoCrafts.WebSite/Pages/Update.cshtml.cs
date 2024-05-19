using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    /// Manage the Update of the data for a single record
    public class UpdateModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        /// Default Construtor
        public UpdateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public Product Item { get; set; }

        /// REST Get request
        /// Loads the Data
        public void OnGet(string id)
        {
            Item = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
            if (Item == null)
            {
                this.ModelState.AddModelError("OnGet Update", "Unable to Update, Item is null");
            }
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

            ProductService.UpdateData(Item);

            return RedirectToPage("./DedicatedIndex");
        }
    }
}