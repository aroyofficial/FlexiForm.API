using FlexiForm.API.Exceptions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FlexiForm.API.Helpers
{
    /// <summary>
    /// Provides utility methods for hashing passwords.
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Generates a SHA-256 hash for the given plain text password.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>A hexadecimal string representation of the hashed password.</returns>
        public static string GetHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            using var sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Convert to hex string
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        /// <summary>
        /// Verifies whether the hash of the provided password matches the expected hash.
        /// </summary>
        /// <param name="password">The plain text password to verify.</param>
        /// <param name="expectedHash">The expected hash value to compare against.</param>
        /// <returns>
        /// <c>true</c> if the hash of the input password matches the expected hash; otherwise, <c>false</c>.
        /// </returns>
        public static bool Verify(string password, string expectedHash)
        {
            var originalHash = GetHash(password);
            return originalHash == expectedHash;
        }

        /// <summary>
        /// Validates whether the given password is strong according to defined rules.
        /// </summary>
        /// <param name="password">The password string to validate.</param>
        public static void CheckStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new PasswordRequiredException();
            }

            var minLength = 8;

            var hasMinimumLength = password.Length >= minLength;
            var hasUpperCase = Regex.IsMatch(password, "[A-Z]");
            var hasLowerCase = Regex.IsMatch(password, "[a-z]");
            var hasDigit = Regex.IsMatch(password, "[0-9]");
            var hasSpecialChar = Regex.IsMatch(password, "[^a-zA-Z0-9]");

            if (!(hasMinimumLength &&
                hasUpperCase &&
                hasLowerCase &&
                hasDigit &&
                hasSpecialChar))
            {
                throw new StrongPasswordRequiredException();
            }
        }
    }
}
