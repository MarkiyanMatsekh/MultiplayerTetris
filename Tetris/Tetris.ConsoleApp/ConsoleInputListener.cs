using System;
using System.Collections.Generic;
using System.Threading;
using Tetris.Core.GameContracts;
using Tetris.Core.GameLogic;

namespace Tetris.ConsoleApp
{
    // currently this class is not reusable after stop. will consider that later
    public class ConsoleInputListener : IUserInputListener
    {
        private static readonly Dictionary<ConsoleKey, MoveType> _mapping = new Dictionary<ConsoleKey, MoveType>()
        {
            {ConsoleKey.LeftArrow, MoveType.MoveLeft},
            {ConsoleKey.RightArrow, MoveType.MoveRight},
            {ConsoleKey.DownArrow, MoveType.TossDown},
            {ConsoleKey.Spacebar, MoveType.Rotate}
        };

        private IInputSerializer _serializer;

        private Thread _listeningThread;
        private bool _continueRunning;

        public void BindInputSerializer(IInputSerializer serializer)
        {
            _serializer = serializer;
        }

        public bool IsListening
        {
            get { return _continueRunning; }
        }

        public void Start()
        {
            if (_serializer == null)
                throw new InvalidOperationException("Cannot start listener without binding input serializer");

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
