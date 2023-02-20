﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.GameLogic.Player;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.GameLogic.WorkSpacing.Commanders;
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

        Wallet CreateWallet();
        Task<Barrel> CreateBarrel(Vector3 position = new Vector3());
        Task<WorkSpace> CreateWorkPlace(Wallet wallet, IPumpingCommander commander, Vector3 position, float yRotation);
    }
}