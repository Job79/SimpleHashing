using System;
using System.Text;
using NUnit.Framework;

namespace SimpleHashing.Test
{
    public class Test
    {
        [Test]
        public void TestHashingOutput()
        {
            var salt = new byte[16];
            var hash = PBKDF2.Hash("test", salt: salt);
            Assert.AreEqual("AAAAAAAAAAAAAAAAAAAAAKdxg3v6h4MevEEaW1d8kjFK+4wW0E2l1gDrVn6R26xa", hash);

            var hash2 = PBKDF2.Hash(Encoding.UTF8.GetBytes("test"), salt: salt);
            Assert.AreEqual("AAAAAAAAAAAAAAAAAAAAAKdxg3v6h4MevEEaW1d8kjFK+4wW0E2l1gDrVn6R26xa", Convert.ToBase64String(hash2));
        }

        [Test]
        public void TestRandomSalt()
        {
           Assert.AreNotEqual(PBKDF2.Hash("test"),PBKDF2.Hash("test")); 
        }

        [Test]
        public void TestVerify()
        {
            var hash = PBKDF2.Hash("test");
            Assert.IsFalse(PBKDF2.Verify(hash,"test1234"));
            Assert.IsTrue(PBKDF2.Verify(hash,"test"));
        }
    }
}