using UnityEngine;

namespace CodeBase.UI.Transition
{
    public class LeftTransitionButton : TransitionButton
    {
        protected override bool IsNeedToBuy => Company.IsCurrentWorkSpaceFirst;
        
        protected override void ToOtherWorkSpace()
        {
            Company.ToPreviousWorkSpace();
        }
    }
}