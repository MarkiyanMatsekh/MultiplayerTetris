using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Fleck;
using Tetris.ConsoleApp;
using Tetris.Core.GameContracts;
using Tetris.Core.GameLogic;
using Tetris.Core.GameObjects;
using Tetris.WebApp;
using Tetris.WebApp.Dto;

class Program
{
    static void Main()
    {
        FleckLog.Level = LogLevel.Error;

        var renderers = new ConcurrentDictionary<IWebSocketConnection, IRenderer>();

        var proxyRenderer = new ProxyRenderer();
        var allSockets = new List<IWebSocketConnection>();
        var server = new WebSocketServer("ws://localhost:8080/websession");
        server.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                allSockets.Add(socket);
                renderers[socket] = new WebRenderer(socket);
                proxyRenderer.AddRenderer(renderers[socket]);

                if (allSockets.Count == 1)
                {
                    var size = new Size(10, 15);
                    var dto = new InitMessageDto(new SizeDto(size));
                    socket.Send(dto.ToJson());

                    var engine = new GameEngine(size, new ConsoleInputListener(), proxyRenderer);
                    engine.Start();
                }
            };
            socket.OnClose = () =>
            {
                allSockets.Remove(socket);
                proxyRenderer.RemoveRenderer(renderers[socket]);

                IRenderer toRemove;
                renderers.TryRemove(socket, out toRemove);
            };
            socket.OnMessage = message =>
            {
                //Console.WriteLine(message);
                //allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
            };
        });

        while (true)
        {
            Thread.Sleep(1000);
        }


       // Console.ReadLine();
    }
}