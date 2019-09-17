using Windows.Storage;

namespace RineClient.Common
{
    public class RineSettings
    {
        private readonly ApplicationDataContainer _settings = ApplicationData.Current.RoamingSettings;

        static RineSettings()
        {
            Current = new RineSettings();
        }

        public object this[string key]
        {
            get => _settings.Values[key];
            set => _settings.Values[key] = value;
        }

        public static RineSettings Current { get; }

        public object SetDefault(string key, object value)
        {
            if (_settings.Values.TryGetValue(key, out object v))
            {
                return v;
            }
            else
            {
                _settings.Values[key] = value;
                return value;
            }
        }

        public static string TokenUri
        {
            get => (string)Current.SetDefault("TokenUri", "/Token");
        }

        public static string BaseUri
        {
            get => (string)Current.SetDefault("BaseUri", "https://localhost:8000");
        }

        public static string ChatHubUri
        {
            get => (string)Current.SetDefault("ChatHubUri", "/chathub");
        }

        public static string Version => "1.0.0";

        public static string AppName => "Rine UWP";
    }
}
