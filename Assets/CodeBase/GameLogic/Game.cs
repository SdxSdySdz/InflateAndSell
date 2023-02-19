using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.Update;
using CodeBase.Infrastructure.States.Core;

namespace CodeBase.GameLogic
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner, IUpdateService updateService)
        {
            StateMachine = new StateMachine(AllServices.Container, new SceneLoader(coroutineRunner), updateService);
        }

        public StateMachine StateMachine { get; }

        public void Save()
        {
            AllServices.Container.Get<ISaveLoadService>().SaveProgress();
        }
    }
}