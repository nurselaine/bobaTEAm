using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ContosoCrafts.WebSite.Models
{
	public class User
	{
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinedAt { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string ProfilePictureUrl { get; set; } // URL to profile picture or avatar


        public override string ToString() => JsonSerializer.Serialize<User>(this);
    }
}

