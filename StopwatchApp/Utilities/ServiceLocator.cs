using StopwatchApp.ViewModels;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace StopwatchApp.Utilities
{
    public class ServiceLocator
    {
        public ViewModelMain ViewModelMain
        {
            get { return new ViewModelMain(new AppSettings(), new Stopwatch(), new DispatcherTimer()); }
        }

    }
}
