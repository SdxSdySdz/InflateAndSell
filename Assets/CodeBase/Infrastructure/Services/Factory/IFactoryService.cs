using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IFactoryService : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressWriter> ProgressWriters { get; }
        
        void Cleanup();
        Task WarmUp();
        
        Task<Barrel> CreateBarrel(Vector3 position = new Vector3());
        Task<WorkSpace> CreateWorkPlace(Vector3 position, float yRotation);
    }
}