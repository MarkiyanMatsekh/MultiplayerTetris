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
            {ConsoleKey.DownArrow, MoveType.MoveDown},
            {ConsoleKey.Spacebar, MoveType.Rotate}
        };

        private IInputQueue _queue;

        private Thread _listeningThread;
        private bool _continueRunning;

        public void BindInputSerializer(IInputQueue queue)
        {
            _queue = queue;
        }

        public bool IsListening
        {
            get { return _continueRunning; }
        }

        public void Start()
        {
            if (_queue == null)
                throw new InvalidOperationException("Cannot start listener without binding input queue");

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
            MoveType move;
            while (true)
            {
                // todo: use Console.KeyAvailable 
                var key = Console.ReadKey(true);

                if (!_continueRunning)
                    break;


                if (_mapping.TryGetValue(key.Key,out move))
                    _queue.Enqueue(move);
            } 
        }
    }
}
