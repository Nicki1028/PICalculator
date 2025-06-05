using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PI_Calculator.Models
{
    public class PIModel : INotifyPropertyChanged
    {
        private long _samplesize;
        public long SampleSize
        {
            get => _samplesize; 
            set
            {
                _samplesize = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime _time;
        public DateTime Time
        {
            get => _time; set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

        private double _value;
        public double Value
        {
            get => _value; set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        public PIModel(long sampleSize, DateTime time, double value)
        {
            this.SampleSize = sampleSize;
            this.Time = time;
            this.Value = value;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
