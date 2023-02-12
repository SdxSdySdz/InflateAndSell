using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
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
    }
}