namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        void Enable();
        void Disable();
        bool IsClicked(out ClickTarget clickTarget);
    }
}