using System;
using System.Threading;

namespace Snake
{
    public class TimerProvider
    {
        private Timer _timer;
        private readonly int _intervalInMilliseconds = 250;

        public void StartTimer(Action onTimer)
        {
            _timer = new Timer(state =>
            {
                onTimer();
            }, null, _intervalInMilliseconds, _intervalInMilliseconds);
        }
    }
}
