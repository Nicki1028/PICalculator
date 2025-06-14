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
        private string _status;
        public string Status
        {
            get => _status; 
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        private CancellationTokenSource? _cancellationTokenSource;
        public CancellationTokenSource? CancellationTokenSource
        {
            get => _cancellationTokenSource;
            set
            {
                _cancellationTokenSource = value;
                OnPropertyChanged();
            }
        }
        public PIModel(long sampleSize, DateTime time, double value, string status, CancellationTokenSource? cancellationTokenSource)
        {
            this.SampleSize = sampleSize;
            this.Time = time;
            this.Value = value;
            this.Status = status;
            this.CancellationTokenSource = cancellationTokenSource;
        }
        public PIModel(long sampleSize, DateTime time, double value)
        {
            this.SampleSize = sampleSize;
            this.Time = time;
            this.Value = value;
            this.Status = "Pending";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
