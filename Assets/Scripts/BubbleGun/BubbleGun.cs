using System.Collections;
using Zenject;
using UnityEngine;

namespace Bubbles.BubbleGun
{
    [RequireComponent(typeof(PlayerTouchDetector))]
    [RequireComponent(typeof(RandomBubbleGenerator))]
    public class BubbleGun : MonoBehaviour
    {
        [SerializeField] private float _secondsToGenerateBubble;
        [SerializeField] private int _bubblesToShootCount;

        [SerializeField] private BubbleColorType[] _projectilesQueue;
        [SerializeField] private ProjectileBubble _projectileBubblePrefab;

        [Inject] private PauseController _pauseController;
        private ProjectileBubble _currentProjectileBubble;

        private PlayerTouchDetector _playerTouchDetector;
        private IBubblesGenerator _bubbleGenerator;
        private void Awake()
        {
            _playerTouchDetector = GetComponent<PlayerTouchDetector>();
            _bubbleGenerator = GetComponent<IBubblesGenerator>();
        }
        private void Start()
        {
            var newBubbleColor = _bubbleGenerator.GenerateBubble();
            SpawnProjectileBubble(newBubbleColor);
        }
        private void OnEnable()
        {

            _playerTouchDetector.OnTouchFineshed += TryThrowBubble;
        }
        private void TryThrowBubble(Vector3 playerTouchPosition)
        {
            if (_currentProjectileBubble == null) return;
            var moveDirection = playerTouchPosition - transform.position;
            _currentProjectileBubble.StartMoving(moveDirection.normalized);

            _currentProjectileBubble.OnCollisionWithBubble += StartGenerateNewBubble;
        }
        private void StartGenerateNewBubble()
        {
            if (_currentProjectileBubble == null) return;
            _currentProjectileBubble.OnCollisionWithBubble -= StartGenerateNewBubble;

            _currentProjectileBubble = null;
            StartCoroutine(GeneratingBubble());
        }
        private IEnumerator GeneratingBubble()
        {
            yield return new WaitForSeconds(_secondsToGenerateBubble);
            var newBubbleColor = _bubbleGenerator.GenerateBubble();
            SpawnProjectileBubble(newBubbleColor);
        }
        private void SpawnProjectileBubble(BubbleColorType color)
        {
            var newBubble = Instantiate(_projectileBubblePrefab, transform.position, Quaternion.identity);

            newBubble.InjectedPauseController = _pauseController;
            newBubble.BubbleColor = color;

            _currentProjectileBubble = newBubble;
        }
        private void OnDisable()
        {
            _playerTouchDetector.OnTouchFineshed -= TryThrowBubble;
        }
    }
}