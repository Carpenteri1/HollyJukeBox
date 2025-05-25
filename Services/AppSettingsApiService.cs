using Microsoft.Extensions.Options;

namespace HollyJukeBox.Services;

public class AppSettingsApiService
{
        private readonly ApiSettings _settings;

        public AppSettingsApiService(IOptions<ApiSettings> options)
        {
            _settings = options.Value;
        }

        /*public void UseSettings()
        {
            Console.WriteLine(_settings.ApiKey);
        }*/
}