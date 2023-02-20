using System;
using CodeBase.GameLogic.WorkSpacing.Commanders;

namespace CodeBase.Data
{
    [Serializable]
    public class WorkSpaceProgress
    {
        public PumpingCommanderType CommanderType;

        public WorkSpaceProgress(IPumpingCommander commander)
        {
            switch (commander)
            {
                case EmployeeCommander:
                    CommanderType = PumpingCommanderType.Employee;
                    break;
                
                case InputBasedCommander:
                    CommanderType = PumpingCommanderType.InputBased;
                    break;
                
                default:
                    throw new ArgumentException(nameof(commander));
            }
        }
    }
}