namespace API.Modules
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddModule<TModule>(this IServiceCollection services, IConfiguration configuration) where TModule : IModule, new()
        {
            var module = new TModule();
            module.Register(services, configuration);
            return services;
        }
    }
}
