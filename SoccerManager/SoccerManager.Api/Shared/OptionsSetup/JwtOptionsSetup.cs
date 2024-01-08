using Microsoft.Extensions.Options;

namespace SoccerManager.Api.Shared.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _config;

        public JwtOptionsSetup(IConfiguration config)
        {
            _config = config;
        }

        public void Configure(JwtOptions options)
        {
            _config.GetSection("JwtSettings").Bind(options);
        }
    }
}
