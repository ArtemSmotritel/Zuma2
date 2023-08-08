using System;
using System.Windows.Threading;

namespace Zuma.controllers
{
    internal class GameFlowController
    {

        public DispatcherTimer GameFlowTimer { get; private set; }
        
        public GameFlowController()
        {
            GameFlowTimer = new DispatcherTimer();
            GameFlowTimer.Interval = TimeSpan.FromMilliseconds(16);
            GameFlowTimer.Start();
        }

        public void SubscribeToGameFlowTimer(EventHandler action)
        {
            GameFlowTimer.Tick += action;
        }

    }
}
