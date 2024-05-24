using System.Collections.Generic;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    public class DedicatedIndexModel : PageModel
    {
        public DedicatedIndexModel(JsonFileMenuServices productService)
        {
            ProductService = productService;
        }

        public JsonFileMenuServices ProductService { get; }
        public IEnumerable<MenuItem>? Products { get; private set; }

        public void OnGet() => Products = ProductService.GetMenuItems();
    }
}
