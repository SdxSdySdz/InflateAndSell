using CodeBase.Data;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
    
        private readonly IProgressService _progressService;
        private readonly IFactoryService _gameFactory;

        public SaveLoadService(IProgressService progressService, IFactoryService gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (IProgressWriter progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }
      
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
        }
    }
}