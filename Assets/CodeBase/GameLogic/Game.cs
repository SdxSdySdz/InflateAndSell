using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States.Core;

namespace CodeBase.GameLogic
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new StateMachine(AllServices.Container, new SceneLoader(coroutineRunner));
        }

        public StateMachine StateMachine { get; }

        public void Save()
        {
            AllServices.Container.Get<ISaveLoadService>().SaveProgress();
        }
    }
}