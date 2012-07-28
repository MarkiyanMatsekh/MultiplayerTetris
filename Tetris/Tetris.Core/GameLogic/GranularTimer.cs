using System;
using System.Threading;

namespace Tetris.Core.GameLogic
{
    public class GranularTimer
    {
        private readonly int _granularity;
        private readonly Action _callback;
        private readonly int _tickInterval;
        private readonly Timer _timer;
        
        private int _ticksCounter;

        public GranularTimer(Action callback, int tickInterval, int granularity)
        {
            if (callback == null) 
                throw new ArgumentNullException("callback");
            _callback = callback;
            _tickInterval = tickInterval;
            _granularity = granularity;

            _timer = new Timer(OnTimerCallback, null, Timeout.Infinite, _tickInterval / _granularity);
        }

        private void OnTimerCallback(object state)
        {
            _ticksCounter = (_ticksCounter + 1) % _granularity;

            if (_ticksCounter == 0)
                _callback();
        }

        public void Start()
        {
            _timer.Change(0, _tickInterval/_granularity);
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}