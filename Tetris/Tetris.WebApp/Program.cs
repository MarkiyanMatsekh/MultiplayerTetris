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
using Tetris.ConsoleApp;
using Tetris.Core.GameLogic;
using Tetris.Core.GameObjects;
using Tetris.WebApp;

class Program
{
    static void Main()
    {
        FleckLog.Level = LogLevel.Debug;

        var proxyRenderer = new ProxyRenderer();
        var allSockets = new List<IWebSocketConnection>();
        var server = new WebSocketServer("ws://localhost:8080/websession");
        server.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                Console.WriteLine("Open!");
                allSockets.Add(socket);
                proxyRenderer.AddRenderer(new WebRenderer(socket));
            };
            socket.OnClose = () =>
            {
                Console.WriteLine("Close!");
                allSockets.Remove(socket);
                proxyRenderer.RemoveRenderer(new WebRenderer(socket));
            };
            socket.OnMessage = message =>
            {
                Console.WriteLine(message);
                allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
            };
        });

        proxyRenderer.AddRenderer(new ConsoleRenderer());

        var engine = new GameEngine(new Size(10, 10),
                                new WebInputListener(), 
                                proxyRenderer,
                                () =>
                                {
                                    Console.Clear();
                                    Console.WriteLine("Game Over!");
                                });
        engine.Start();

    }
}