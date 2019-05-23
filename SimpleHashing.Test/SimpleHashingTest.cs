/* SimpleHashing.Test
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace SimpleHashing.Test
{
    [TestClass]
    public class SimpleHashingTest
    {
        [TestMethod]
        public void TestString()
        {
            const string input = "12345";
            string hash1 = PBKDF2.Hash(input);
            bool hash1Equals = PBKDF2.Verify(hash1, input);

            string hash2 = PBKDF2.Hash(input, 100);
            bool hash2Equals = PBKDF2.Verify(hash2, input, 100);

            string hash3 = PBKDF2.Hash(input, length: 60);
            bool hash3Equals = PBKDF2.Verify(hash3, input);

            Assert.IsTrue(hash1Equals&&hash2Equals&&hash3Equals);
        }

        [TestMethod]
        public void TestByte()
        {
            byte[] input = Encoding.UTF8.GetBytes("12345");

            byte[] hash1 = PBKDF2.Hash(input);
            bool hash1Equals = PBKDF2.Verify(hash1, input);

            byte[] hash2 = PBKDF2.Hash(input, 100);
            bool hash2Equals = PBKDF2.Verify(hash2, input, 100);

            byte[] hash3 = PBKDF2.Hash(input, length: 60);
            bool hash3Equals = PBKDF2.Verify(hash3, input);

            Assert.IsTrue(hash1Equals && hash2Equals && hash3Equals);
        }
    }
}
