using System;
using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;

namespace Tetris.Core.GameLogic
{
    public class GameEngine
    {
        private readonly IUserInputListener _inputListener;
        
        private readonly MoveHandler _moveHandler;
        private readonly GranularTimer _timer;
        private readonly InputQueue _inputQueue;
        private readonly Action _onGameOver;

        public GameEngine(Size fieldSize, IUserInputListener listener, IRenderer renderer, Action onGameOver = null )
        {
            _inputQueue = new InputQueue(HandleMovement);
            _timer = new GranularTimer(OnTimerCallback, 1000, 4);
            _onGameOver = onGameOver;
            var gameField = new GameField(fieldSize, _inputQueue);
            _moveHandler = new MoveHandler(gameField, renderer);

            _inputListener = listener;
            _inputListener.BindInputSerializer(_inputQueue);
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
            _moveHandler.HandleMove(move);
        }
    }
}
