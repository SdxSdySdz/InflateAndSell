namespace CodeBase.Infrastructure.Services.Update
{
    public interface IUpdateService : IService
    {
        void Register(IUpdatable updatable);
        void Unregister(IUpdatable updatable);
    }
}