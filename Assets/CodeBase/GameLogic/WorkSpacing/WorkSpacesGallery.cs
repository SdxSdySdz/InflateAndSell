using System.Collections;
using System.Collections.Generic;

namespace CodeBase.GameLogic.WorkSpacing
{
    public class WorkSpacesGallery : IEnumerable<WorkSpace>
    {
        private readonly LinkedList<WorkSpace> _workSpaces;

        public WorkSpacesGallery(IEnumerable<WorkSpace> workSpaces)
        {
            _workSpaces = new LinkedList<WorkSpace>(workSpaces);
        }

        public void AddLast(WorkSpace workSpace)
        {
            _workSpaces.AddLast(workSpace);
        }
        
        public void AddFirst(WorkSpace workSpace)
        {
            _workSpaces.AddLast(workSpace);
        }

        public IEnumerator<WorkSpace> GetEnumerator()
        {
            return _workSpaces.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}