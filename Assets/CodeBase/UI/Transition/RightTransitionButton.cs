namespace CodeBase.UI.Transition
{
    public class RightTransitionButton : TransitionButton
    {
        protected override bool IsNeedToBuy => Company.IsCurrentWorkSpaceLast;
        
        protected override void ToOtherWorkSpace()
        {
            Company.ToNextWorkSpace();
        }
    }
}