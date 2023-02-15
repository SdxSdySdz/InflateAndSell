namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        bool IsClicked(out ClickTarget clickTarget);
    }
}