using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using ContosoCrafts.WebSite.Services;

public class LoginModel : PageModel
{
    [BindProperty]
    [Required]
    public string Username { get; set; }

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    private readonly BobaUserService bobaUserService;

    public LoginModel(IWebHostEnvironment webHostEnvironment)
    {
        bobaUserService = new BobaUserService(webHostEnvironment);
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var users = bobaUserService.LoadUsers();
        var user = users.FirstOrDefault(u => u.Username == Username);

        if (user != null && user.VerifyPassword(Password))
        {
            return RedirectToPage("/Index");
        }
        else
        {
            ErrorMessage = "Invalid username or password.";
            return Page();
        }
    }
}