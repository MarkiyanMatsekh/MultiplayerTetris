using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Tetris.Contracts;
using Tetris.Implementation.Figures;

namespace Tetris.ConsoleApp.classes
{
    // currently this class is not reusable after stop. will consider that later
    public class UserInputListener
    {
        private static readonly Dictionary<ConsoleKey, MoveType> _mapping = new Dictionary<ConsoleKey, MoveType>()
        {
            {ConsoleKey.LeftArrow, MoveType.MoveLeft},
            {ConsoleKey.RightArrow, MoveType.MoveRight},
            {ConsoleKey.DownArrow, MoveType.TossDown},
            {ConsoleKey.Spacebar, MoveType.Rotate}
        };

        private readonly IInputSerializer _serializer;

        private Thread _listeningThread;
        private bool _continueRunning;

        public UserInputListener(IInputSerializer serializer)
        {
            _serializer = serializer;
        }

        public void Start()
        {
            if (_listeningThread != null)
                throw new InvalidOperationException("listener is already running");

            _listeningThread = new Thread(ReadFromConsole);
            _listeningThread.IsBackground = true;
            _listeningThread.Name = "User Input Listening Thread";
            
            _continueRunning = true;
            _listeningThread.Start();

        }

        public void Stop()
        {
            _continueRunning = false;
        }

        private void ReadFromConsole()
        {
            while (true)
            {
                // todo: use Console.KeyAvailable 
                var key = Console.ReadKey();

                if (!_continueRunning)
                    break;

                var move = _mapping[key.Key];
                _serializer.Enqueue(move);
            } 
        }
    }
}
