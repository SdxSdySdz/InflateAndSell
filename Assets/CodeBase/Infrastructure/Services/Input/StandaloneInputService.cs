using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Infrastructure.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        private bool _isEnabled;

        public StandaloneInputService()
        {
            Disable();
        }
        
        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        public bool IsClicked(out ClickTarget clickTarget)
        {
            clickTarget = ClickTarget.Unknown;
            
            if (_isEnabled == false)
                return false;
            
            if (EventSystem.current != null)
                clickTarget = EventSystem.current.currentSelectedGameObject ? ClickTarget.UI : ClickTarget.Unknown;
            
            return UnityEngine.Input.GetMouseButtonDown(0);
        }
    }
}