using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.ConsoleApp.classes
{
    public class GameEngine
    {
        private readonly GranularTimer _timer;
        private readonly UserInputListener _inputListener;
        private readonly InputSerializer _inputSerializer;
        private readonly IMoveHandler _moveHandler;

        public GameEngine()
        {
            _inputSerializer = new InputSerializer(HandleMovement);
            _inputListener = new UserInputListener(_inputSerializer);
            _moveHandler = new MoveHandler(new GameField(10, 10), _inputSerializer);
            _timer = new GranularTimer(OnTimerCallback, 1000, 4);
        }

        public void Start()
        {
            _timer.Start();
            _inputListener.Start();
            _inputSerializer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _inputListener.Stop();
            _inputSerializer.Stop();
        }

        private void OnTimerCallback()
        {
            _inputSerializer.Enqueue(MoveType.MoveDown);
        }

        private void HandleMovement(MoveType move)
        {
            _moveHandler.HandleMove(move);
        }
    }
}
