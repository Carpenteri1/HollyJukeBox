using Microsoft.Extensions.Options;

namespace JukeBox.Services;

public class AppSettingsApiService
{
        private readonly ApiSettings _settings;

        public AppSettingsApiService(IOptions<ApiSettings> options)
        {
            _settings = options.Value;
        }
}