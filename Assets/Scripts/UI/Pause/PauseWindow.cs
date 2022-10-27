using Zenject;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PauseWindow : MonoBehaviour
{
    [Inject] private PauseController _pauseController;

    private Animator _myAnimator;
    private readonly int isShowingKey = Animator.StringToHash("is-showing");
    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _pauseController.OnPauseStateChanged += UpdateWindowState;
    }
    private void UpdateWindowState(bool state)
    {
        _myAnimator.SetBool(isShowingKey, state);
    }
    private void OnDisable()
    {
        _pauseController.OnPauseStateChanged -= UpdateWindowState;
    }
}
