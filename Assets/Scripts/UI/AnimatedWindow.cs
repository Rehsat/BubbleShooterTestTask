using UnityEngine;

public abstract class AnimatedWindow : MonoBehaviour
{
    private Animator _myAnimator;
    private readonly int isShowingKey = Animator.StringToHash("is-showing");
    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }
    protected void UpdateWindowShowingState(bool state)
    {
        _myAnimator.SetBool(isShowingKey, state);
    }
}
