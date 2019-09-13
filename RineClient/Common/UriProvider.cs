
namespace RineClient.Common
{
    class UriProvider
    {
        private static RineSettings _settings = new RineSettings();

        public static string TokenUri
        {
            get => (string)_settings.SetDefault("TokenUri", "/Token");
        }

        public static string BaseUri
        {
            get => (string)_settings.SetDefault("BaseUri", "https://localhost:8000");
        }

        public static string ChatHubUri
        {
            get => (string)_settings.SetDefault("ChatHubUri", "/chathub");
        }
    }
}
