using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Infrastructure.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        public bool IsClicked(out ClickTarget clickTarget)
        {
            if (EventSystem.current != null)
                clickTarget = EventSystem.current.currentSelectedGameObject ? ClickTarget.UI : ClickTarget.Unknown;
            else
                clickTarget = ClickTarget.Unknown;
            
            return UnityEngine.Input.GetMouseButtonDown(0);
        }
    }
}