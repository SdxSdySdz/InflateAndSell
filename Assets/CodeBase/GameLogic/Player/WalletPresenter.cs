using TMPro;
using UnityEngine;

namespace CodeBase.GameLogic.Player
{
    public class WalletPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Wallet _wallet;

        public void Construct(Wallet wallet)
        {
            _wallet = wallet;
            OnEnable();
        }

        private void OnEnable()
        {
            if (_wallet != null)
                _wallet.Changed += UpdateView;
        }

        private void OnDisable()
        {
            if (_wallet != null)
                _wallet.Changed -= UpdateView;
        }

        private void UpdateView(int amount)
        {
            _text.text = $"{amount} $";
        }
    }
}