using System;
using System.Security.Cryptography;
using System.Text;

namespace SimpleHashing
{
    public static class PBKDF2
    {
        private const int DefaultIterations = 50000;
        private const int DefaultLength = 32;
        private const int SaltLength = 16;

        /// <summary>
        /// Hash a byte array with PBKDF2
        /// </summary>
        /// <param name="input"></param>
        /// <param name="iterations"></param>
        /// <param name="length">length of hash, output will also contain the salt</param>
        /// <param name="salt">generated if null</param>
        /// <returns>salt + hash</returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] Hash(byte[] input, int iterations = DefaultIterations, int length = DefaultLength,
            byte[] salt = null)
        {
            if (input == null || input.Length <= 0) throw new ArgumentException("Input is empty");
            if (iterations <= 0) throw new ArgumentException("Iterations can't be negative or 0");
            if (length <= 0) throw new ArgumentException("Length can't be negative or 0");
            if(salt != null && salt.Length != SaltLength) throw new ArgumentException("Salt has an invalid length");

            // Create salt of 16 bytes if salt is null.
            salt ??= GenerateRandomSalt(SaltLength);

            // Create hash of [length] bytes.
            using var rfc = new Rfc2898DeriveBytes(input, salt, iterations);
            var hash = rfc.GetBytes(length);

            // Combine salt and hash.
            var combinedBytes = new byte[SaltLength + length];
            Buffer.BlockCopy(salt, 0, combinedBytes, 0, SaltLength); // Add salt
            Buffer.BlockCopy(hash, 0, combinedBytes, 16, length); // Add hash

            // Return combinedBytes as base64 string
            return combinedBytes;
        }

        /// <summary>
        /// Hash a string with PBKDF2
        /// </summary>
        /// <param name="input"></param>
        /// <param name="iterations"></param>
        /// <param name="length">length of hash, output will also contain the salt</param>
        /// <param name="salt">generated if null</param>
        /// <returns>salt + hash encoded with Base64</returns>
        public static string Hash(string input, int iterations = DefaultIterations, int length = DefaultLength,
            byte[] salt = null)
            => Convert.ToBase64String(Hash(Encoding.UTF8.GetBytes(input), iterations, length, salt));

        /// <summary>
        /// Determine whether the hash is equal to input
        /// </summary>
        /// <param name="hashedInput">salt + generated hash</param>
        /// <param name="input">array to compare with hashedInput</param>
        /// <param name="iterations"></param>
        /// <returns>true if hash is equal to input</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool Verify(byte[] hashedInput, byte[] input, int iterations = DefaultIterations)
        {
            if (hashedInput == null || hashedInput.Length <= SaltLength)
                throw new ArgumentException("Couldn't verify hash: hashedInput is invalid");

            // Get salt from input
            var salt = new byte[SaltLength];
            Buffer.BlockCopy(hashedInput, 0, salt, 0, SaltLength);

            // Create hash
            var hash = Hash(input, iterations, hashedInput.Length - SaltLength, salt);

            // Compare hashes
            for (int i = 0; i < hashedInput.Length; i++)
                if (hashedInput[i] != hash[i])
                    return false;
            return true;
        }
        
        /// <summary>
        /// Determine whether the hash is equal to input
        /// </summary>
        /// <param name="hashedInput">salt + generated hash</param>
        /// <param name="input">string to compare with hashedInput</param>
        /// <param name="iterations"></param>
        /// <returns>true if hash is equal to input</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool Verify(string hashedInput, string input, int iterations = DefaultIterations)
            => Verify(Convert.FromBase64String(hashedInput), Encoding.UTF8.GetBytes(input), iterations);

        /// <summary>
        /// Generate new random salt 
        /// </summary>
        /// <param name="length">length of random data</param>
        /// <returns>byte array with random data</returns>
        private static byte[] GenerateRandomSalt(int length)
        {
            var salt = new byte[length];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);
            return salt;
        }
    }
}