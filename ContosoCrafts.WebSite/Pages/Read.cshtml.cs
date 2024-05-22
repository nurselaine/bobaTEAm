using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages
{
    public class ReadModel : PageModel
    {
        public JsonFileProductService ProductService { get; }

        public ReadModel(JsonFileProductService productService) 
        {
            ProductService = productService;
        }

        public Product Item;

        public IActionResult OnGet(string id)
        {
            Item = ProductService.GetProducts().FirstOrDefault(x => x.Id.Equals(id));
            if (Item == null)
            {
                this.ModelState.AddModelError("OnGet Read", "Unable to Read, Item is null");
                return RedirectToPage("./DedicatedIndex");
            }
            return Page();
        }
    }
}
