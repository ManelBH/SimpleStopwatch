using System.Configuration;


namespace StopwatchApp.Utilities
{
    public class AppSettings : IAppSettings
    {
        public string TimeFormat { get => ConfigurationManager.AppSettings["TimeFormat"]; }
    }
}
