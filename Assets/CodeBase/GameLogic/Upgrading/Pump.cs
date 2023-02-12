using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.GameLogic.Upgrading
{
    public class Pump : MonoBehaviour, IUpgradeable
    {
        private Hose _hose;
        private Lever _lever;
        
        public void Upgrade()
        {
            _hose.Upgrade();
            _lever.Upgrade();
        }
    }
}