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
            const string Input = "12345";
            string Hash1 = PBKDF2.Hash(Input);
            bool Hash1Equals = PBKDF2.Verify(Hash1, Input);

            string Hash2 = PBKDF2.Hash(Input, 100);
            bool Hash2Equals = PBKDF2.Verify(Hash2, Input, 100);

            string Hash3 = PBKDF2.Hash(Input, Length: 60);
            bool Hash3Equals = PBKDF2.Verify(Hash3, Input);

            Assert.IsTrue(Hash1Equals&&Hash2Equals&&Hash3Equals);
        }

        [TestMethod]
        public void TestByte()
        {
            byte[] Input = Encoding.UTF8.GetBytes("12345");

            byte[] Hash1 = PBKDF2.Hash(Input);
            bool Hash1Equals = PBKDF2.Verify(Hash1, Input);

            byte[] Hash2 = PBKDF2.Hash(Input, 100);
            bool Hash2Equals = PBKDF2.Verify(Hash2, Input, 100);

            byte[] Hash3 = PBKDF2.Hash(Input, Length: 60);
            bool Hash3Equals = PBKDF2.Verify(Hash3, Input);

            Assert.IsTrue(Hash1Equals && Hash2Equals && Hash3Equals);
        }
    }
}
