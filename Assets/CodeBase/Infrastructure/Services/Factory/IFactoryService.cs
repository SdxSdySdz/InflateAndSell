using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.GameLogic;
using CodeBase.GameLogic.Upgrading;
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
        
        Task<Barrel> CreateBarrel();
        Task<Pump> CreatePump();
    }
}