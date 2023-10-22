
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
using System.IO;
using System.Text;

namespace TestProject.Domain
{
    public class NamedPipeStreamReader
    {
        private Stream stream;
        private Encoding streamEncoding;

        public NamedPipeStreamReader(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            this.stream = stream;
            streamEncoding = new UnicodeEncoding();
        }

        public string Read()
        {
            int size = stream.ReadByte() * 256;
            size += stream.ReadByte();

            byte[] buffer = new byte[size];

            stream.Read(buffer, 0, size);

            return streamEncoding.GetString(buffer);
        }
    }
}
