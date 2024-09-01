
namespace Domain.Helper;

public static class PasswordHashHelper
{
    public static string HashPassword(this string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPasswordAsync(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}