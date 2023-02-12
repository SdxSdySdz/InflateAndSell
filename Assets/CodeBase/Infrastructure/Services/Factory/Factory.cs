using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Constants;
using CodeBase.GameLogic;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.States.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class Factory : IFactoryService
    {
        private readonly IAssetsService _assets;
        private readonly IProgressService _persistentProgressService;
        private readonly StateMachine _stateMachine;

        public Factory(
            IAssetsService assets,
            IProgressService persistentProgressService,
            StateMachine stateMachine)
        {
            _assets = assets;
            _persistentProgressService = persistentProgressService;
            _stateMachine = stateMachine;
        }


        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressWriter> ProgressWriters { get; } = new List<IProgressWriter>();

        public async Task WarmUp()
        {
/*await _assets.Load<GameObject>(AssetAddress.Loot);
await _assets.Load<GameObject>(AssetAddress.Spawner);*/
        }

        public async Task<Barrel> CreateBarrel()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Barrel);
            Barrel barrel = InstantiateRegistered(prefab).GetComponent<Barrel>();

// barrel.Construct();

            return barrel;
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