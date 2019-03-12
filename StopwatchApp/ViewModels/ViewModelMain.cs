using StopwatchApp.Utilities;
using System;
using System.Diagnostics;

using System.Windows.Threading;

namespace StopwatchApp.ViewModels
{
    public class ViewModelMain : ViewModelBase
    {
        private const double updateFrequency = 1D / 30; // 30Hz

        private readonly string _timeFormat = @"hh\:mm\:ss";
        private readonly string _blinkTimeFormat;
        private readonly Stopwatch _stopwatch;
        private readonly DispatcherTimer _updateTimer;

        public RelayCommand Start { get; set; }
        public RelayCommand Stop { get; set; }
        public RelayCommand Reset { get; set; }

        private string _elapsedTime;
        public string ElapsedTime {
            get => _elapsedTime;
            set
            {
                _elapsedTime = value;
                RaisePropertyChanged("ElapsedTime");
            }
        }

        public string BackgroundTime { get; }

        public ViewModelMain(IAppSettings appSettings, Stopwatch stopwatch, DispatcherTimer updateTimer)
        {
            _timeFormat = appSettings.TimeFormat?? _timeFormat;
            _blinkTimeFormat = _timeFormat.Replace(':', ' ');
            _elapsedTime = TimeSpan.Zero.ToString(_timeFormat);
            BackgroundTime = _elapsedTime.Replace('0', '8');

            _stopwatch = stopwatch;
            _updateTimer = updateTimer;
            _updateTimer.Interval = TimeSpan.FromMilliseconds(updateFrequency); 
            _updateTimer.Tick += (o, e) => UpdateElapsedTime(true);

            Start = new RelayCommand(DoStart);
            Stop = new RelayCommand(DoStop);
            Reset = new RelayCommand(DoReset);
        }

        private void DoStart(object param)
        {
            _stopwatch.Start();
            _updateTimer.Start();
        }

        private void DoStop(object param)
        {
            _stopwatch.Stop();
            _updateTimer.Stop();
            UpdateElapsedTime();
        }

        private void DoReset(object param)
        {
            _stopwatch.Stop();
            _updateTimer.Stop();
            _stopwatch.Reset();
            UpdateElapsedTime();
        }

        private void UpdateElapsedTime(bool blink = false)
        {
            if (blink && _stopwatch.Elapsed.Seconds % 2 == 0)
                ElapsedTime = _stopwatch.Elapsed.ToString(_blinkTimeFormat);
            else
                ElapsedTime = _stopwatch.Elapsed.ToString(_timeFormat);
        }


    }
}
