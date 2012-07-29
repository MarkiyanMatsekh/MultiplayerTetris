using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Tetris.Core.GameLogic
{
    public interface IInputQueue
    {
        void Enqueue(MoveType move);
        void Clear();
    }

    public class InputQueue : IInputQueue
    {
        private readonly Action<MoveType> _callback;
        private readonly ConcurrentQueue<MoveType> _movesQueue;

        private Thread _runningThread;
        private bool _continueRunning;

        public InputQueue(Action<MoveType> callback)
        {
            _callback = callback;
            _movesQueue = new ConcurrentQueue<MoveType>();
        }

        public void Enqueue(MoveType move)
        {
            _movesQueue.Enqueue(move);
        }

        public void Clear()
        {
            MoveType notImportantMove;
            while (!_movesQueue.IsEmpty)
            {
                _movesQueue.TryDequeue(out notImportantMove);
            }
        }

        public void Start()
        {
            if (_runningThread != null)
                throw new InvalidOperationException("InputQueue is already running");

            _runningThread = new Thread(ReadFromQueue);
            _runningThread.Name = "Input Serializer Thread";
            _runningThread.Priority = ThreadPriority.AboveNormal;

            _continueRunning = true;
            _runningThread.Start();
        }

        public void Stop()
        {
            _continueRunning = false;
        }

        private void ReadFromQueue()
        {
            MoveType move;
            while (true)
            {
                if (!_continueRunning)
                    break;

                if (_movesQueue.TryDequeue(out move))
                    _callback(move);
                else
                    Thread.Sleep(100);
            }
        }
    }
}