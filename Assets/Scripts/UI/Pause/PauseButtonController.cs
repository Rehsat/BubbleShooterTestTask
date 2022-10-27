using Zenject;
using UnityEngine.UI;
using UnityEngine;

namespace Bubbles.UI
{
    public class PauseButtonController : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private bool _pauseState;

        [Inject] private PauseController _pauseController;

        private void OnEnable()
        {
            _button.onClick.AddListener(SetPauseState);
        }
        public void SetPauseState()
        {
            _pauseController.SetPauseState(_pauseState);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(SetPauseState);
        }
    }
}
