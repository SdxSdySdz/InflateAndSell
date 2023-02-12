namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        bool IsClicked { get; }
    }
}