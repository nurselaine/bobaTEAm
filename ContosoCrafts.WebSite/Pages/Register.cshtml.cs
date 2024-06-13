using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

public class RegisterModel : PageModel
{
    public BobaUserService UserService { get; }
    public LoginModel LoginModel { get; }
    public List<BobaUser> Users { get; set; }
    public BobaUser User { get; set; }

    public RegisterModel(BobaUserService userService)
    {
        UserService = userService;
    }

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
    [Url]
    [Display(Name = "Profile Picture URL")]
    public string? ProfilePictureUrl { get; set; }

    public void OnGet()
    {
        User = new BobaUser()
        {
            UserId = Guid.NewGuid().ToString(),
            Username = Username,
            FirstName = FirstName,
            LastName = LastName,
            JoinedAt = DateTime.Now,
            ProfilePictureUrl = ProfilePictureUrl ?? "",
            IsAdmin = false
        };
        // Users = bobaUserService.LoadUsers();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        User = new BobaUser()
        {
            UserId = Guid.NewGuid().ToString(),
            Username = Username,
            FirstName = FirstName,
            LastName = LastName,
            JoinedAt = DateTime.Now,
            ProfilePictureUrl = ProfilePictureUrl ?? "",
            IsAdmin= true,
            IsLoggedIn = false
        };
        User.HashPassword(Password);

        UserService.CreateData(User);

        return RedirectToPage("/Login");
    }
}