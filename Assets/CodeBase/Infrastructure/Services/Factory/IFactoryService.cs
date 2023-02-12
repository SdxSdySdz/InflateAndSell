namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IFactoryService : IService
    {
        Barrel CreateBarrel();
    }
}