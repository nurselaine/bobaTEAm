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

        public void OnGet(string id)
        {
            Item = ProductService.GetProducts().First(x => x.Id == id);
        }
    }
}
