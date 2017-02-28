
/*
The MIT License(MIT)

Copyright(c) 2017 Denis Lebedev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;

namespace TestProject.Domain.Tests
{
    [TestClass()]
    public class NamedPipeStreamWriterTests
    {
        private const string TestString = "Cowards die many times before their deaths; the valiant never taste of death but once.";

        [TestMethod()]
        public void WriteTest()
        {
            var encoding = new UnicodeEncoding();
            var ms = new MemoryStream();
        
            var pipeStream = new NamedPipeStreamWriter(ms);

            pipeStream.Write(TestString);

            ms.Position = 2;

            byte[] buffer = new byte[172];

            ms.Read(buffer, 0, 172);

            string result = encoding.GetString(buffer);

            Assert.AreEqual(TestString, result);
        }
    }
}