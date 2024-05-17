using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using ContosoCrafts.WebSite.Services;
using System.Collections.Generic;

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

    private readonly BobaUserService bobaUserService;

    public RegisterModel(IWebHostEnvironment webHostEnvironment)
    {
        bobaUserService = new BobaUserService(webHostEnvironment);
    }

    public List<BobaUser> Users { get; set; }

    public void OnGet()
    {
        Users = bobaUserService.LoadUsers();
    }

    public IActionResult OnPost(string username, string password,
        string firstName, string lastName, string profilePictureUrl)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var users = bobaUserService.LoadUsers();

        var newUser = new BobaUser
        {
            UserId = Guid.NewGuid().ToString(),
            Username = username,
            FirstName = firstName,
            LastName = lastName,
            JoinedAt = DateTime.Now,
            ProfilePictureUrl = profilePictureUrl
        };
        newUser.HashPassword(password);
        users.Add(newUser);

        bobaUserService.SaveUsers(users);

        return RedirectToPage("/Login");
    }
}