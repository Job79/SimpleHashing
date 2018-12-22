using System;
using System.Security.Cryptography;

namespace Hashing
{
    class PBKDF2
    {
        public static string Hash(string Password, int Iterations = 10000, int Length = 20)
        {
            //create salt of 16 byte's
            byte[] Salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(Salt);

            //create hash of 20 byte's
            byte[] Hash = new Rfc2898DeriveBytes(Password, Salt, Iterations).GetBytes(Length);

            //combine salt and hash
            var CombinedBytes = new byte[16+Length];
            Array.Copy(Salt, 0, CombinedBytes, 0, 16);
            Array.Copy(Hash, 0, CombinedBytes, 16, Length);

            //return string
            return Convert.ToBase64String(CombinedBytes);
        }

        public static bool Verify(string Password, string HashedPassword, int Iterations = 10000, int Length = 20)
        {
            //convert to byte's
            byte[] HashedBytes = Convert.FromBase64String(HashedPassword);

            //get the salt from byte's
            byte[] Salt = new byte[16];
            Array.Copy(HashedBytes, 0, Salt, 0, 16);

            //generate hash of password, to compare it with hashedPassword
            byte[] hash = new Rfc2898DeriveBytes(Password, Salt, 10000).GetBytes(Length);

            //compare the result's
            for (int i = 0; i < 20; i++)
                if (HashedBytes[i + 16] != hash[i]) return false;
            return true;
        }
    }
}
