using Mapster;

namespace K8S.DriverAPI.Profiles
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            // Register Mapster profiles or configurations
            TypeAdapterConfig.GlobalSettings.Scan(typeof(MapsterProfile).Assembly);
        }
    }
}
