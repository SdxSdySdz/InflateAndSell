using UnityEngine.EventSystems;

namespace CodeBase.Infrastructure.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        public bool IsClicked(out ClickTarget clickTarget)
        {
            clickTarget = EventSystem.current.currentSelectedGameObject ? ClickTarget.UI : ClickTarget.Unknown;

            return UnityEngine.Input.GetMouseButtonDown(0);
        }
    }
}