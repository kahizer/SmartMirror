using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv.Services
{
    using System.Threading;

    using SmartMirrorWinUniv.Concreates;

    public class DateTimeServices
    {

        #region Fields

        private Timer timer;

        private int elapseTime = 5000;

        #endregion

        #region Constructor

        public DateTimeServices()
        {
            this.timer = new Timer(this.UpdateTime, null, 0, Timeout.Infinite);    
        }

        #endregion

        #region Public Events

        public event EventHandler<CurrentTime> TimerUpdateEvent;

        #endregion

        private void UpdateTime(object state)
        {
            this.TimerUpdateEvent?.Invoke(this, new CurrentTime(DateTime.Now));
            this.timer.Change(this.elapseTime, Timeout.Infinite);
        }
    }
}
