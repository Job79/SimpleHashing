using System;
using System.Text;
using NUnit.Framework;

namespace SimpleHashing.Test
{
    public class Test
    {
        [Test]
        public static void TestHashing()
        {
            var salt = new byte[16];
            var hash = PBKDF2.Hash("test", salt: salt);
            Console.WriteLine(hash);

            var hash2 = PBKDF2.Hash(Encoding.UTF8.GetBytes("test"), salt: salt);
            Console.WriteLine(Convert.ToBase64String(hash2));
        }
    }
}