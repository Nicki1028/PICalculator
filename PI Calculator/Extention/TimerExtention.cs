using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace PI_Calculator.Extention
{
    public static class TimerExtention
    {
        private static System.Threading.Timer Timer = null;
        private static FrameworkElement control = null;
        public static void Timerextention(this Window window, Action callback)
        {
            TimerExtention.control = window;
            TimerExtention.control.Tag = callback;
            if (Timer == null)
            {
                Timer = new System.Threading.Timer(TimerCallback, null, 1000, Timeout.Infinite);
            }
            else
            {
                Timer.Change(1000, Timeout.Infinite);
            }

        }

        private static void TimerCallback(object data)
        {

            control.Dispatcher.Invoke(() => {
                Action callback = (Action)control.Tag;
                callback?.Invoke();
            });

        }
    }
}
