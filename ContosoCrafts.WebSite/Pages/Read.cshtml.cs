using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages
{
    public class ReadModel : PageModel
    {
        public JsonFileMenuServices ProductService { get; }

        public ReadModel(JsonFileMenuServices productService) 
        {
            ProductService = productService;
        }

        public MenuItem Item;

        public IActionResult OnGet(string name)
        {
            Item = ProductService.GetMenuItems().FirstOrDefault(x => x.Name.Equals(name));
            if (Item == null)
            {
                this.ModelState.AddModelError("OnGet Read", "Unable to Read, Item is null");
                return RedirectToPage("./DedicatedIndex");
            }
            return Page();
        }
    }
}
