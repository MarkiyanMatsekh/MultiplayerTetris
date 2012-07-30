using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Tetris.WebApp
{
    internal static class StupidWebSocketServer
    {
        public static void Start()
        {
            var listener = new TcpListener(IPAddress.Loopback, 8080);
            listener.Start();
            while (true)
            {
                using (var client = listener.AcceptTcpClient())
                using (var stream = client.GetStream())
                {
                    var headers = new Dictionary<string, string>();
                    string line = String.Empty;
                    while ((line = ReadLine(stream)) != String.Empty)
                    {
                        var tokens = line.Split(new char[] { ':' }, 2);
                        if (!String.IsNullOrWhiteSpace(line) && tokens.Length > 1)
                        {
                            headers[tokens[0]] = tokens[1].Trim();
                        }
                    }

                    var magicString = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                    var key = headers["Sec-WebSocket-Key"];

                    var acceptKey = CalculateAcceptKey(key + magicString, new ASCIIEncoding());

                    var response =
                        "HTTP/1.1 101 Switching Protocols" + Environment.NewLine +
                        "Upgrade: websocket" + Environment.NewLine +
                        "Connection: Upgrade" + Environment.NewLine +
                        "Sec-WebSocket-Accept: " + acceptKey + Environment.NewLine +
                        Environment.NewLine;

                    var bufferedResponse = Encoding.UTF8.GetBytes(response);
                    stream.Write(bufferedResponse, 0, bufferedResponse.Length);


                    byte[] buffer = new byte[4096];
                    var reader = new BinaryReader(stream);
                    var writer = new BinaryWriter(stream);
                    while (true)
                    {
                        var bytesRead = reader.Read(buffer, 0, 4096);

                        var str = Encoding.UTF8.GetString(buffer,0,bytesRead);

                        //writer.Write("you send me this: " + str);
                    }

                }
            }
        }

        private static string ReadLine(Stream stream)
        {
            var sb = new StringBuilder();
            var buffer = new List<byte>();
            while (true)
            {
                buffer.Add((byte)stream.ReadByte());
                var line = Encoding.ASCII.GetString(buffer.ToArray());
                if (line.EndsWith(Environment.NewLine))
                {
                    return line.Substring(0, line.Length - 2);
                }
            }
        }

        private static string CalculateAcceptKey(string text, Encoding enc)
        {
            byte[] buffer = enc.GetBytes(text);
            var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            var hash = cryptoTransformSHA1.ComputeHash(buffer);

            string returnValue = Convert.ToBase64String(hash);
            return returnValue;
        }

    }
}
