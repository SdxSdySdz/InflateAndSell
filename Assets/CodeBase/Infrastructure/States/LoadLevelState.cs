using CodeBase.Infrastructure.States.Core;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : State, IIndependentState
    {
        private readonly SceneLoader _sceneLoader;
        
        public LoadLevelState(StateMachine stateMachine, SceneLoader sceneLoader) : base(stateMachine)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(Scenes.GameLoop, EnterGameLoop);
        }

        public void Exit()
        {
        }

        private void EnterGameLoop()
        {
            StateMachine.Enter<GameLoopState>();
        }
    }
}