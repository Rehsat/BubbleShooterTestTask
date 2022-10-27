using Zenject;
using UnityEngine;

namespace Bubbles.UI
{
    [RequireComponent(typeof(Animator))]
    public class PauseWindow : AnimatedWindow
    {
        [Inject] private PauseController _pauseController;

        private void OnEnable()
        {
            _pauseController.OnPauseStateChanged += UpdateWindowShowingState;
        }
        private void OnDisable()
        {
            _pauseController.OnPauseStateChanged -= UpdateWindowShowingState;
        }
    }
}
