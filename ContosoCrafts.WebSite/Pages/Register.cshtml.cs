using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;

public class RegisterModel : PageModel
{
    [BindProperty]
    [Required]
    [Display(Name = "Username (case sensitive)")]
    public string Username { get; set; }

    [BindProperty]
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [BindProperty]
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }

    [BindProperty]
    [Required]
    [Url]
    [Display(Name = "Profile Picture URL")]
    public string ProfilePictureUrl { get; set; }

    public string ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var users = BobaUser.LoadUsers();
        if (users.Any(u => u.Username == Username))
        {
            ErrorMessage = "Username already exists.";
            return Page();
        }

        var newUser = new BobaUser
        {
            UserId = Guid.NewGuid().ToString(),
            FirstName = FirstName,
            LastName = LastName,
            JoinedAt = DateTime.Now,
            Username = Username,
            ProfilePictureUrl = ProfilePictureUrl
        };
        newUser.HashPassword(Password);
        users.Add(newUser);
        BobaUser.SaveUsers(users);

        return RedirectToPage("/Login");
    }
}