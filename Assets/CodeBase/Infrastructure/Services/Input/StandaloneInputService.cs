namespace CodeBase.Infrastructure.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        public bool IsClicked => UnityEngine.Input.GetMouseButtonDown(0);
    }
}