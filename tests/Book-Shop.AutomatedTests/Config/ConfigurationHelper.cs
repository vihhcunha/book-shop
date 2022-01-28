using Microsoft.Extensions.Configuration;
using System.IO;

namespace Book_Shop.AutomatedTests.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _configuration;

        public ConfigurationHelper()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
        public string WebDriversFolder => _configuration.GetSection("WebDrivers").Value;
        public string DomainUrl => _configuration.GetSection("DomainUrl").Value;
        public string FolderPath => Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        public string RegisterUrl => $"{DomainUrl}{_configuration.GetSection("RegisterUrl").Value}";
        public string LoginUrl => $"{DomainUrl}{_configuration.GetSection("LoginUrl").Value}";
        public string ProviderListUrl => $"{DomainUrl}{_configuration.GetSection("ProviderListUrl").Value}";
        public string ProductListUrl => $"{DomainUrl}{_configuration.GetSection("ProductListUrl").Value}";
    }
}
