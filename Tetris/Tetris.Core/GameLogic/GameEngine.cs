using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;

namespace Tetris.Core.GameLogic
{
    public class GameEngine
    {
        private readonly IUserInputListener _inputListener;
        
        private readonly MoveHandler _moveHandler;
        private readonly GranularTimer _timer;
        private readonly InputSerializer _inputSerializer;

        public GameEngine(Size fieldSize, IUserInputListener listener, IRenderer renderer )
        {
            _inputSerializer = new InputSerializer(HandleMovement);
            _moveHandler = new MoveHandler(new GameField(fieldSize), _inputSerializer, renderer);
            _timer = new GranularTimer(OnTimerCallback, 1000, 4);
            
            _inputListener = listener;
            _inputListener.BindInputSerializer(_inputSerializer);
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
