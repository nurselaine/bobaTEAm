using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ContosoCrafts.WebSite.Models
{
	public class BobaUser
	{
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime JoinedAt { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public Boolean? IsAdmin { get; set; }

        public Boolean? IsLoggedIn { get; set; }

        public override string ToString() => JsonSerializer.Serialize<BobaUser>(this);
        
        // Method to hash the password using SHA256
        public void HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute hash from the password string
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                PasswordHash = sb.ToString();
            }
        }

        // Method to verify if the provided password matches the stored hash
        public bool VerifyPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute hash from the provided password
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                string hashedPassword = sb.ToString();

                // Compare the hashed password with the stored hash
                return hashedPassword.Equals(PasswordHash);
            }
        }
    }
}

