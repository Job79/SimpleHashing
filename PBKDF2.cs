using System;
using System.Security.Cryptography;

namespace encryption
{
    class PBKDF2
    {
        public static string Hash(string password, int iterations = 10000)
        {
            //create salt of 16 byte's
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //create hash of 20 byte's
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(20);

            //combine salt and hash
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //return string
            return Convert.ToBase64String(hashBytes);
        }

        public static bool Verify(string password, string hashedPassword)
        {
            //convert to byte's
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            //get the salt from byte's
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            
            //generate hash of password, to compare it with hashedPassword
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            
            //compare the result's
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
