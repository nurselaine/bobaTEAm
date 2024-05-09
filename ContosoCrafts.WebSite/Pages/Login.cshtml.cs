using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


public class LoginModel : PageModel
{
    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        //PLEASE HELP
        var users = User.LoadUser();
        var user = users.FirstOrDefault(u => u.Username == Username);

        if (user != null && user.VerifyPassword(Password))
        {
            // Logic for successful login
            ErrorMessage = "Login successful.";
        }
        else
        {
            // Logic for failed login
            ErrorMessage = "Invalid username or password.";
        }
    }

    private bool IsValidUser(string email, string password)
    {
        // dummy validation logic
        return !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password);
    }
}
