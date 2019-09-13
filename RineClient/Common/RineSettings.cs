using Windows.Storage;

namespace RineClient.Common
{
    public class RineSettings
    {
        private readonly ApplicationDataContainer _settings = ApplicationData.Current.RoamingSettings;

        public object this[string key]
        {
            get => _settings.Values[key];
            set => _settings.Values[key] = value;
        }

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
    }
}
