using FlexiForm.API.Internals;
using FlexiForm.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace FlexiForm.API.Helpers
{
    /// <summary>
    /// Provides utility methods for securely generating, hashing, and verifying one-time passwords (OTPs) using dynamic salts.
    /// </summary>
    public static class OTPHelper
    {
        /// <summary>
        /// Generates a random 6-digit numeric OTP, hashes it with a cryptographically secure salt,
        /// and returns both the plain and hashed values along with the salt.
        /// </summary>
        /// <returns>
        /// A <see cref="ProcessedOTP"/> object containing the plain OTP, its hashed value, and the generated salt.
        /// </returns>
        public static ProcessedOTP Generate()
        {
            const int length = 6;
            const string digits = "0123456789";
            var otp = new char[length];

            using var rng = RandomNumberGenerator.Create();
            byte[] buffer = new byte[1];

            for (int i = 0; i < length; i++)
            {
                do
                {
                    rng.GetBytes(buffer);
                }
                while (buffer[0] >= digits.Length * (byte.MaxValue / digits.Length)); // ensure uniform distribution

                otp[i] = digits[buffer[0] % digits.Length];
            }

            var payload = new OTPPayload()
            {
                OTP = new string(otp),
                Salt = GenerateSalt()
            };

            return ProcessOTP(payload);
        }

        /// <summary>
        /// Generates a cryptographically secure random salt encoded as a Base64 string.
        /// </summary>
        /// <param name="size">The number of bytes to use for the salt. Default is 16.</param>
        /// <returns>A Base64-encoded string representing the generated salt.</returns>
        private static string GenerateSalt(int size = 16)
        {
            var buffer = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Computes the SHA-256 hash of the OTP combined with its salt and returns a structured result.
        /// </summary>
        /// <param name="payload">
        /// An <see cref="OTPPayload"/> containing the plain OTP and salt values.
        /// If the salt is not provided, one will be generated.
        /// </param>
        /// <returns>
        /// A <see cref="ProcessedOTP"/> object containing the plain OTP, its hash, and the salt.
        /// </returns>
        private static ProcessedOTP ProcessOTP(OTPPayload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload), "Payload cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(payload.OTP))
            {
                throw new ArgumentException("OTP cannot be null or empty.", nameof(payload.OTP));
            }

            if (string.IsNullOrWhiteSpace(payload.Salt))
            {
                throw new ArgumentException("Salt cannot be null or empty.", nameof(payload.Salt));
            }

            payload.Salt = string.IsNullOrWhiteSpace(payload.Salt) ? GenerateSalt() : payload.Salt;
            using var sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(payload.OTP + payload.Salt);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            var otp = new ProcessedOTP()
            {
                Value = payload.OTP,
                Hash = sb.ToString(),
                Salt = payload.Salt
            };

            return otp;
        }

        /// <summary>
        /// Verifies a plain OTP input against a stored hash and salt from an <see cref="OTP"/> entity.
        /// </summary>
        /// <param name="otp">The <see cref="OTP"/> object containing the stored hash and salt.</param>
        /// <param name="expectedOTP">The plain OTP string to verify.</param>
        /// <returns>
        /// <c>true</c> if the hashed OTP matches the stored hash; otherwise, <c>false</c>.
        /// </returns>
        public static bool Verify(this OTP otp, string expectedOTP)
        {
            if (otp == null)
            {
                throw new ArgumentNullException(nameof(otp), "OTP cannot be null.");
            }

            var payload = new OTPPayload()
            {
                OTP = expectedOTP,
                Salt = otp.Salt
            };

            var processedOTP = ProcessOTP(payload);
            return processedOTP.Hash == otp.Value;
        }
    }
}
