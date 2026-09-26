namespace API.Modules
{
    public interface IModule
    {
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}
