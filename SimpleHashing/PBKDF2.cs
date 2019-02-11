/* SimpleHashing
 * Copyright (C) 2019  henkje (henkje@pm.me)
 * 
 * MIT license
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
        const int DEFAULT_ITERATIONS = 50000;
        const int DEFAULT_LENGTH = 32;

        /// <summary>
        /// Hash a string with PBKDF2.
        /// </summary>
        /// <param name="Input">String to hash</param>
        /// <param name="Iterations">Rounds PBKDF2 will make to genarete the hash</param>
        /// <param name="Length">Lenth of the hash, the output will also contains the salt</param>
        /// <returns>The generated salt+hash</returns>
        public static string Hash(string Input, int Iterations = DEFAULT_ITERATIONS, int Length = DEFAULT_LENGTH)
        {
            if (string.IsNullOrEmpty(Input)) throw new Exception("Could not hash input: Input is empty.");
            else if (Iterations <= 0) throw new Exception("Could not hash input: Iterations can't be negative or null.");
            else if (Length <= 0) throw new Exception("Could not hash input: Length can't be negative or null.");

            //Create salt of 16 byte's.
            byte[] Salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(Salt);

            //Create hash of 20 byte's.
            byte[] Hash = new Rfc2898DeriveBytes(Input, Salt, Iterations).GetBytes(Length);

            //Combine salt and hash.
            byte[] CombinedBytes = new byte[16 + Length];
            Buffer.BlockCopy(Salt, 0, CombinedBytes, 0, 16);
            Buffer.BlockCopy(Hash, 0, CombinedBytes, 16, Length);

            //Return CombinedBytes as base64 string.
            return Convert.ToBase64String(CombinedBytes);
        }

        /// <summary>
        /// Hash a byte[] with PBKDF2.
        /// </summary>
        /// <param name="Input">Byte[] to hash</param>
        /// <param name="Iterations">Rounds PBKDF2 will make to genarete the hash</param>
        /// <param name="Length">Lenth of the hash, the output will also contains the salt</param>
        /// <returns>The generated salt+hash</returns>
        public static byte[] Hash(byte[] Input, int Iterations = DEFAULT_ITERATIONS, int Length = DEFAULT_LENGTH)
        {
            if (Input == null) throw new Exception("Could not hash input: Input is null.");
            else if (Iterations <= 0) throw new Exception("Could not hash input: Iterations can't be negative or null.");
            else if (Length <= 0) throw new Exception("Could not hash input: Length can't be negative or null.");

            //Create salt of 16 byte's.
            byte[] Salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(Salt);

            //Create hash of 20 byte's.
            byte[] Hash = new Rfc2898DeriveBytes(Input, Salt, Iterations).GetBytes(Length);

            //Combine salt and hash.
            byte[] CombinedBytes = new byte[16 + Length];
            Buffer.BlockCopy(Salt, 0, CombinedBytes, 0, 16);
            Buffer.BlockCopy(Hash, 0, CombinedBytes, 16, Length);

            //Return CombinedBytes;
            return CombinedBytes;
        }

        /// <summary>
        /// Verify hashed string with another string.
        /// </summary>
        /// <param name="HashedInput">Hashed string</param>
        /// <param name="Input">String to compare with HashedInput</param>
        /// <param name="Iterations">Rounds PBKDF2 made to generate HashedInput</param>
        /// <returns>boolean, true if HashedInput is the same as Input</returns>
        public static bool Verify(string HashedInput, string Input, int Iterations = DEFAULT_ITERATIONS)
        {
            if (string.IsNullOrEmpty(Input)) throw new Exception("Could not hash input: HashedInput is empty.");
            else if (string.IsNullOrEmpty(Input)) throw new Exception("Could not hash input: Input is empty.");
            else if (Iterations <= 0) throw new Exception("Could not hash input: Iterations can't be negative or null.");

            //Get byte's from HashedInput.
            byte[] HashedBytes = Convert.FromBase64String(HashedInput);

            //Get the length of the hash.
            int Length = HashedBytes.Length - 16;
            Console.WriteLine(Length);

            //Get the salt from HashedInputBytes.
            byte[] Salt = new byte[16];
            Buffer.BlockCopy(HashedBytes, 0, Salt, 0, 16);

            //Generate hash of Input, to compare it with HashedInput
            byte[] Hash = new Rfc2898DeriveBytes(Input, Salt, Iterations).GetBytes(Length);

            //Compare the result's.
            for (int i = 0; i < Length; i++)
                if (HashedBytes[i + 16] != Hash[i]) return false;

            return true;
        }

        /// <summary>
        /// Verify hashed byte[] with another byte[].
        /// </summary>
        /// <param name="HashedInput">Hashed string</param>
        /// <param name="Input">String to compare with HashedInput</param>
        /// <param name="Iterations">Rounds PBKDF2 made to generate HashedInput</param>
        /// <returns>boolean, true if HashedInput is the same as Input</returns>
        public static bool Verify(byte[] HashedInput, byte[] Input, int Iterations = DEFAULT_ITERATIONS)
        {
            if (HashedInput == null) throw new Exception("Could not hash input: HashedInput is empty.");
            else if (Input == null) throw new Exception("Could not hash input: Input is empty.");
            else if (Iterations <= 0) throw new Exception("Could not hash input: Iterations can't be negative or null.");

            //Get byte's from HashedInput.
            byte[] HashedBytes = HashedInput;

            //Get the length of the hash.
            int Length = HashedBytes.Length - 16;
            Console.WriteLine(Length);

            //Get the salt from HashedInputBytes.
            byte[] Salt = new byte[16];
            Buffer.BlockCopy(HashedBytes, 0, Salt, 0, 16);

            //Generate hash of Input, to compare it with HashedInput
            byte[] Hash = new Rfc2898DeriveBytes(Input, Salt, Iterations).GetBytes(Length);

            //Compare the result's.
            for (int i = 0; i < Length; i++)
                if (HashedBytes[i + 16] != Hash[i]) return false;

            return true;
        }
    }
}
