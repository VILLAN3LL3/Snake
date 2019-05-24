using System;
using System.Threading;

namespace Snake
{
    public class TimerProvider
    {
        private Timer _timer;
        private int _intervalInMilliseconds = 500;

        public void StartTimer(Action onTimer)
        {
            _timer = new Timer(state =>
            {
                onTimer();
            }, null, _intervalInMilliseconds, _intervalInMilliseconds);
        }

        public void SpeedUpTimer()
        {
            _intervalInMilliseconds = Math.Max(50, _intervalInMilliseconds -= 50);
            _timer.Change(_intervalInMilliseconds, _intervalInMilliseconds);
        }
    }
}
