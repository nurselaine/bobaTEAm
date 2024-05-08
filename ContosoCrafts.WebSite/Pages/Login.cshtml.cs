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
        if (!IsValidUser(Email, Password))
        {
            ErrorMessage = "Invalid login attempt. Please try again.";
        }
        else
        {
            // dummy variables
            Email = string.Empty;
            Password = string.Empty;
            ErrorMessage = "Login successful!";
        }
    }

    private bool IsValidUser(string email, string password)
    {
        // dummy validation logic
        return !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password);
    }
}
