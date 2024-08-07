using Microsoft.AspNetCore.Mvc;

namespace AdminUI.Controllers
{
    public class ConfigController : Controller
    {
        private readonly IConfiguration _configuration;

        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("script/settings.js")]
        public ContentResult SettingsJs()
        {
            var jsContent = $@"
                const settings = {{
                    UserAuthenticationUrl: '{_configuration["HttpClients:UserAuthenticationUrl"]}',
                    CartManagementUrl: '{_configuration["HttpClients:CartManagementUrl"]}',
                    CatalogManagementUrl: '{_configuration["HttpClients:CatalogManagementUrl"]}',
                    OrderManagementUrl: '{_configuration["HttpClients:OrderManagementUrl"]}'
                }};
                export default settings;
            ";
            return Content(jsContent, "application/javascript");
        }
    }
}