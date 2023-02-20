namespace CodeBase.GameLogic.WorkSpacing.Commanders
{
    public interface IPumpingCommander
    {
        void Settle(WorkSpace workSpace);
        void Enable();
        void Disable();
    }
}