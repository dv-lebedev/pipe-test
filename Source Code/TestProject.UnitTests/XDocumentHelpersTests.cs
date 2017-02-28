
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
using System.Linq;

namespace System.Xml.Linq.Tests
{
    [TestClass()]
    public class XDocumentHelpersTests
    {
        [TestMethod()]
        public void GetTagsValuesWithTest()
        {
            GetTagsValuesWith("data.xml", "info", "true", new[] { "груша", "яблоко" });

            GetTagsValuesWith("guitars.xml", "country", "USA",
                new[] { "Gibson Les Paul Classic", "Fender Telecaster", "Gibson Thunderbird" });
        }

        private static void GetTagsValuesWith(string file, string attributeName, 
            string attributeValue, string[] values)
        {
            var document = XDocument.Load(file);
            var result = document.GetTagsValuesWith(attributeName, attributeValue);

            Assert.AreEqual(values.Length, result.Length);

            foreach (var item in values)
            {
                if (!result.Contains(item)) Assert.Fail();
            }
        }
    }
}