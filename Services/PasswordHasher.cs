using BCrypt.Net;

namespace DnDNoteKeeper.Services
{
    public static class PasswordHasher
    {
        // Hash password for db
        public static string HashPassword(string password) => 
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        // Compare password for login
        public static bool VerifyPassword(string password, string hash) => 
            BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }
}