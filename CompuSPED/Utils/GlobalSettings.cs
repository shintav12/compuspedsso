using System.Web.Configuration;

namespace CompuSPED.Utils
{
    public class GlobalSettings
    {
        private static GlobalSettings _settings;
        public string RedirectionSBURL { get; set; }

        protected GlobalSettings()
        {
            RedirectionSBURL = WebConfigurationManager.AppSettings["SPRedirectionURL"];
        }

        public static GlobalSettings GetGlobalSettings()
        {
            return _settings ?? (_settings = new GlobalSettings());
        }
    }
}