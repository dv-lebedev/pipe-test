
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

using System.IO.Pipes;
using System.Security.Principal;

namespace TestProject.Domain
{
    public static class NamedPipeUtils
    {
        public static void SendMsg(string pipeName, string msg)
        {
            using (var server = new NamedPipeServerStream(pipeName, PipeDirection.InOut))
            {
                server.WaitForConnection();

                var reader = new NamedPipeStreamReader(server);

                var clientsMsg = reader.Read();

                if (clientsMsg == "GET data.xml")
                {
                    var writer = new NamedPipeStreamWriter(server);

                    server.RunAsClient(() => { writer.Write(msg); });
                }
            }
        }

        public static string ReceiveMsg(string serverName, string pipeName)
        {
            using (var client = new NamedPipeClientStream(serverName, pipeName,
                PipeDirection.InOut, PipeOptions.None,
                TokenImpersonationLevel.Impersonation))
            {
                client.Connect();

                var writer = new NamedPipeStreamWriter(client);
                writer.Write("GET data.xml");

                var reader = new NamedPipeStreamReader(client);
                return reader.Read();
            }
        }
    }
}
