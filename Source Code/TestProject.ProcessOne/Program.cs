
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

using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestProject.Domain;

namespace TestProject.ProcessOne
{
    class Program
    {
        private const string ProcessTwoName = "TestProject.ProcessTwo";
        private const string File = "data.xml";

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Process 1";

                Console.WriteLine("Checking for Process 2...\n");
                RunProcessTwo();

                Console.WriteLine($"Reading {File}...\n");
                var xmlData = GetXmlData(File);
                var msg = ArraryToString(xmlData);

                Console.WriteLine("Sending data...\n");
                NamedPipeUtils.SendMsg("testproject", msg);

                Console.WriteLine("Done.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void RunProcessTwo()
        {
            var processTwo = Process.GetProcessesByName(ProcessTwoName).FirstOrDefault();

            if (processTwo == null)
            {
                Process.Start($"{ProcessTwoName}.exe");
            }
        }

        static string[] GetXmlData(string file)
        {
            var xmlDocument = XDocument.Load(file);

            return xmlDocument.GetTagsValuesWith("info", "true");
        }

        static string ArraryToString(string[] data)
        {
            var sb = new StringBuilder();

            foreach (var item in data)
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }
    }
}
