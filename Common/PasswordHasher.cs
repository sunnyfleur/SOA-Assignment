using System.Security.Cryptography;
using System.Text;

namespace SOA_Assignment.Common
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            using (var hmac = new HMACSHA256())
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
                var enteredHash = Convert.ToBase64String(hash);
                return enteredHash == storedHash;
            }
        }


    }
}