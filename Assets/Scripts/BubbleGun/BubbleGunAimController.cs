using UnityEngine;

namespace Bubbles.BubbleGun
{
    [RequireComponent(typeof(PlayerTouchDetector))]
    public class BubbleGunAimController : MonoBehaviour
    {
        [SerializeField] private Transform _aim;
        [SerializeField] private float _rotationCorrection;

        private PlayerTouchDetector _detector;
        private void Awake()
        {
            _detector = GetComponent<PlayerTouchDetector>();
        }
        private void OnEnable()
        {
            _detector.OnLastTouchPositionChanged += RotateAimToLastTouchPositon;
            _detector.OnTouchFineshed += HideAim;
        }
        private void RotateAimToLastTouchPositon(Vector3 lastTouchPosition)
        {
            _aim.gameObject.SetActive(true);
            var difference = lastTouchPosition - _aim.position;
            difference.Normalize();

            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            _aim.rotation = Quaternion.Euler(0f, 0f, rotationZ + _rotationCorrection);
        }
        private void HideAim(Vector3 lastTouchPosition)
        {
            _aim.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            _detector.OnLastTouchPositionChanged -= RotateAimToLastTouchPositon;
            _detector.OnTouchFineshed -= HideAim;
        }
    }
}
