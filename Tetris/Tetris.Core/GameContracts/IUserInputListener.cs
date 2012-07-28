using Tetris.Core.GameLogic;

namespace Tetris.Core.GameContracts
{
    public interface IUserInputListener
    {
        // todo: this stinks a lot. try figure out smth better
        void BindInputSerializer(IInputSerializer serializer);
        bool IsListening { get; }
        void Start();
        void Stop();
    }
}