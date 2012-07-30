using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Fleck;
using Tetris.WebApp;

class Program
{
    static void Main()
    {
        FleckLog.Level = LogLevel.Debug;
        var allSockets = new List<IWebSocketConnection>();
        var server = new Fleck.WebSocketServer("ws://localhost:8080/websession");
        server.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                Console.WriteLine("Open!");
                allSockets.Add(socket);
            };
            socket.OnClose = () =>
            {
                Console.WriteLine("Close!");
                allSockets.Remove(socket);
            };
            socket.OnMessage = message =>
            {
                Console.WriteLine(message);
                allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
            };
        });

        var input = Console.ReadLine();
        while (input != "exit")
        {
            foreach (var socket in allSockets.ToList())
            {
                socket.Send(input);
            }
            input = Console.ReadLine();
        }

        //StupidWebSocketServer.Start();

        //var wss = new WebSocketServer(8080,
        //                      "file://",
        //                      "ws://localhost:8080/websession");
        //wss.Start();

        //Console.ReadLine();
    }
}