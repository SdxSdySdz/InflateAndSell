using System;
using System.Collections;

namespace CodeBase.Infrastructure.Services.SDK
{
    public interface IYandexGamesService : IService
    {
        void Initialize(Action onSuccess = null);
    }

    public class YandexGamesService : IYandexGamesService
    {
        public void Initialize(Action onSuccess = null)
        {
            var initProcess = ProvideInitialization(onSuccess);
            while (initProcess.MoveNext())
            {
            }
        }

        private IEnumerator ProvideInitialization(Action onSuccess = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            yield return YandexGamesSdk.Initialize();
#endif  
            onSuccess?.Invoke();
            yield break;
        }
    }
}