using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Constants;
using CodeBase.GameLogic.Marketing;
using CodeBase.GameLogic.Player;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.GameLogic.WorkSpacing.Commanders;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class Factory : IFactoryService
    {
        private readonly IAssetsService _assets;

        public Factory(IAssetsService assets)
        {
            _assets = assets;
        }

        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetAddress.Barrel);
            await _assets.Load<GameObject>(AssetAddress.WorkSpace);
        }

        public Wallet CreateWallet()
        {
            Wallet wallet = new Wallet();
            Register(wallet);

            return wallet;
        }

        public async Task<Barrel> CreateBarrel(Vector3 position = new Vector3())
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Barrel);
            Barrel barrel = InstantiateRegistered(prefab).GetComponent<Barrel>();
            barrel.Construct(new Capacity(3));
            
            barrel.transform.position = position;

            return barrel;
        }

        public async Task<WorkSpace> CreateWorkPlace(
            IPumpingCommander commander,
            Market market,
            Wallet wallet,
            Vector3 position,
            float yRotation)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.WorkSpace);
            
            WorkSpace workSpace = InstantiateRegistered(prefab).GetComponent<WorkSpace>();
            workSpace.Construct(commander, market, wallet, this);
            
            workSpace.transform.position = position;
            workSpace.transform.Rotate(0, yRotation, 0);
            
            return workSpace;
        }

        private void Register(IProgressInteractor progressInteractor)
        {
            if (progressInteractor is IProgressWriter progressWriter)
                ProgressWriters.Add(progressWriter);

            if (progressInteractor is IProgressReader progressReader)
                ProgressReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assets.Cleanup();
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgressInteractors(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab);
            RegisterProgressInteractors(gameObject);

            return gameObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
        {
            GameObject gameObject = await _assets.Instantiate(path: prefabPath, at: at);
            RegisterProgressInteractors(gameObject);

            return gameObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject gameObject = await _assets.Instantiate(path: prefabPath);
            RegisterProgressInteractors(gameObject);

            return gameObject;
        }

        private void RegisterProgressInteractors(GameObject gameObject)
        {
            foreach (IProgressInteractor progressReader in gameObject.GetComponentsInChildren<IProgressInteractor>())
            {
                Register(progressReader);
            }
        }
    }
}