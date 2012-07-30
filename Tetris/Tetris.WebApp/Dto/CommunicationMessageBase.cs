namespace Tetris.WebApp.Dto
{
    public abstract class CommunicationMessageBase
    {
        protected CommunicationMessageBase(string msgType)
        {
            this.msgType = msgType;
        }

        public string msgType;
    }
}