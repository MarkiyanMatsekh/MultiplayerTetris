using System;
using System.IO;
using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;

namespace Tetris.Core.GameLogic
{
    class Logger
    {
        private const string _path = "tetris.log";

        public void Init(string text)
        {
            var separator = string.Format("{0}{1}{0}", Environment.NewLine, string.Empty.PadRight(50, '-'));
            var wholeText = string.Format("{0}{1}{2}", separator, text, Environment.NewLine);
            File.AppendAllText(_path, wholeText);
        }

        public void Log(string text, params object[] args)
        {
            var msg = string.Format("{0}: {1}{2}",
                                    DateTime.Now.ToLongTimeString(),
                                    string.Format(text, args),
                                    Environment.NewLine);

            File.AppendAllText(_path, msg);
        }
    }

    public class GameEngine
    {
        private readonly IUserInputListener _inputListener;

        private readonly MoveHandler _moveHandler;
        private readonly GranularTimer _timer;
        private readonly InputQueue _inputQueue;
        private readonly Action _onGameOver;

        private readonly Logger _log;

        public GameEngine(Size fieldSize, IUserInputListener listener, IRenderer renderer, Action onGameOver = null)
        {
            _inputQueue = new InputQueue(HandleMovement);
            _timer = new GranularTimer(OnTimerCallback, 1000, 4);
            _onGameOver = onGameOver;
            var gameField = new GameField(fieldSize, _inputQueue);
            _moveHandler = new MoveHandler(gameField, renderer);

            _inputListener = listener;
            _inputListener.BindInputSerializer(_inputQueue);

            _log = new Logger();
            _log.Init(string.Format("width:{0};height:{1}", fieldSize.Width, fieldSize.Height));
        }

        public void Start()
        {
            _timer.Start();
            _inputListener.Start();
            _inputQueue.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _inputListener.Stop();
            _inputQueue.Stop();
        }

        private void OnTimerCallback()
        {
            _inputQueue.Enqueue(MoveType.MoveDown);
        }

        private void HandleMovement(MoveType move)
        {
            _log.Log(move.ToString());
            _moveHandler.HandleMove(move);
        }
    }
}
