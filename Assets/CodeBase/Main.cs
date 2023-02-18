using System;
using System.Collections;
using Agava.YandexGames;
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
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
            yield break;
        }

        private void OnApplicationQuit()
        {
            _game.Save();
        }
    }
}