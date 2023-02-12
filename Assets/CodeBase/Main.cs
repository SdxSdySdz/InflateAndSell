using CodeBase.GameLogic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase
{
    public class Main : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        } 
    }
}