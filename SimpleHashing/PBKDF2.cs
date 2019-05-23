/* SimpleHashing
 * 
 * Copyright (c) 2019 henkje
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Security.Cryptography;

namespace SimpleHashing
{
    public static class PBKDF2
    {
        private const int defaultIterations = 50000;
        private const int defaultLength = 32;

        /// <summary>
        /// Hash a string with PBKDF2.
        /// </summary>
        /// <param name="input">String to hash</param>
        /// <param name="iterations">Rounds PBKDF2 will make to genarete the hash</param>
        /// <param name="length">Lenth of the hash, the output will also contains the salt</param>
        /// <returns>The generated salt+hash</returns>
        public static string Hash(string input, int iterations = defaultIterations, int length = defaultLength)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Could not hash input: Input is empty.");
            else if (iterations <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or 0.");
            else if (length <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Length can't be negative or 0.");

            //Create salt of 16 byte's.
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            //Create hash of [length] byte's.
            byte[] hash = new Rfc2898DeriveBytes(input, salt, iterations).GetBytes(length);

            //Combine salt and hash.
            byte[] combinedBytes = new byte[16 + length];
            Buffer.BlockCopy(salt, 0, combinedBytes, 0, 16);//Add salt.
            Buffer.BlockCopy(hash, 0, combinedBytes, 16, length);//Add hash.

            //Return combinedBytes as base64 string.
            return Convert.ToBase64String(combinedBytes);
        }

        /// <summary>
        /// Hash a byte[] with PBKDF2.
        /// </summary>
        /// <param name="input">Byte[] to hash</param>
        /// <param name="iterations">Rounds PBKDF2 will make to genarete the hash</param>
        /// <param name="length">Lenth of the hash, the output will also contains the salt</param>
        /// <returns>The generated salt+hash</returns>
        public static byte[] Hash(byte[] input, int iterations = defaultIterations, int length = defaultLength)
        {
            if (input == null)
                throw new ArgumentNullException("Could not hash input: Input is null.");
            else if (iterations <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or 0.");
            else if (length <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Length can't be negative or 0.");

            //Create salt of 16 byte's.
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            //Create hash of [length] byte's.
            byte[] hash = new Rfc2898DeriveBytes(input, salt, iterations).GetBytes(length);

            //Combine salt and hash.
            byte[] combinedBytes = new byte[16 + length];
            Buffer.BlockCopy(salt, 0, combinedBytes, 0, 16);//Add salt.
            Buffer.BlockCopy(hash, 0, combinedBytes, 16, length);//Add hash.

            //Return combinedBytes;
            return combinedBytes;
        }

        /// <summary>
        /// Compare hashed string with another string.
        /// </summary>
        /// <param name="hashedInput">Hashed string</param>
        /// <param name="input">String to compare with hashedInput</param>
        /// <param name="iterations">Rounds PBKDF2 made to generate hashedInput</param>
        /// <returns>boolean, true if hashedInput is the same as input</returns>
        public static bool Verify(string hashedInput, string input, int iterations = defaultIterations)
        {
            if (string.IsNullOrEmpty(hashedInput))
                throw new ArgumentException("Could not hash input: HashedInput is empty.");
            else  if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Could not hash input: Input is empty.");
            else if (iterations <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or 0.");

            //Get byte's from hashedInput.
            byte[] hashedBytes = Convert.FromBase64String(hashedInput);

            //Get the length of the hash.
            int length = hashedBytes.Length - 16;

            //Get the salt from hashedBytes.
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashedBytes, 0, salt, 0, 16);

            //Generate hash of input, to compare it with hashedBytes
            byte[] hash = new Rfc2898DeriveBytes(input, salt, iterations).GetBytes(length);

            //Compare the result's.
            for (int i = 0; i < length; i++)
                if (hashedBytes[i + 16] != hash[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Compare hashed byte[] with another byte[].
        /// </summary>
        /// <param name="hashedBytes">Hashed byte[]</param>
        /// <param name="input">byte[] to compare with hashedBytes</param>
        /// <param name="iterations">Rounds PBKDF2 made to generate hashedBytes</param>
        /// <returns>boolean, true if hashedBytes is the same as input</returns>
        public static bool Verify(byte[] hashedBytes, byte[] input, int iterations = defaultIterations)
        {
            if (hashedBytes == null)
                throw new ArgumentException("Could not hash input: hashedBytes is null.");
            else if (input == null)
                throw new ArgumentException("Could not hash input: Input is empty.");
            else if (iterations <= 0)
               throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or null.");

            //Get the length of the hash.
            int length = hashedBytes.Length - 16;

            //Get the salt from HashedInputBytes.
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashedBytes, 0, salt, 0, 16);

            //Generate hash of Input, to compare it with hashedBytes.
            byte[] hash = new Rfc2898DeriveBytes(input, salt, iterations).GetBytes(length);

            //Compare the result's.
            for (int i = 0; i < length; i++)
                if (hashedBytes[i + 16] != hash[i])
                    return false;

            return true;
        }
    }
}
