namespace ChurchStore.Web
{
    /// <summary>
    /// Instancia globalmente  a classe Configuration
    /// Inicia na classe program
    /// </summary>
    public static class ConfigurationHelper
    {
        public static IConfiguration Config;
        public static string StaticsUri;
        public static string StaticsUriSlider;

        public static void Initialize(IConfiguration Configuration)
        {
            Config = Configuration;

            var indiceUri = Config.GetSection("SITE_StaticsUri").Value; //SITE_StaticsUri = CDN1
            StaticsUri = Config.GetSection(indiceUri ?? "Cdn1").Value;

            var indiceUriSlider = Config.GetSection("SITE_StaticsUriSlider").Value; //SITE_StaticsUriSlider = Statics2
            StaticsUriSlider = Config.GetSection(indiceUriSlider ?? "Statics2").Value;
        }

    }
}
