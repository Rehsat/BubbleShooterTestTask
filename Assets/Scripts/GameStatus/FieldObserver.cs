using UnityEngine;
using System;
using Bubbles.BubbleGun;

namespace Bubbles.GameStatus
{
    [RequireComponent(typeof(IBubblesGenerator))]
    public class FieldObserver : MonoBehaviour
    {
        [SerializeField] private bool _generateOnStart;

        [SerializeField] private Bubble[] _bubbles;
        [SerializeField] private Bubble[] _lastLineOfBubbles;

        [SerializeField] private float _emptyBubblesCount;
        private IBubblesGenerator _bubblesGenerator;

        public Action OnLastLineBubbleIsNotEmpty;
        public Action OnAllBubblesCleared;
        private void Awake()
        {
            _bubblesGenerator = GetComponent<IBubblesGenerator>();
        }
        private void Start()
        {
            if (_generateOnStart)
            {
                foreach (var bubble in _bubbles)
                {
                    var generatedBubble = _bubblesGenerator.GenerateBubble();
                    bubble.BubbleColor = generatedBubble;
                }
            }

            foreach (var bubble in _lastLineOfBubbles)
            {
                bubble.OnBubbleColorChange += CheckLastLineBubble;
            }
            foreach (var bubble in _bubbles)
            {
                bubble.OnBubbleColorChange += CheckBubble;
            }
            _emptyBubblesCount = CalculateCountOfEmptyBubbles();
        }
        private float CalculateCountOfEmptyBubbles()
        {
            var countOfEmptyBubbles = 0;
            foreach (var bubble in _bubbles)
            {
                if (bubble.BubbleColor == BubbleColorType.Empty)
                {
                    countOfEmptyBubbles++;
                }
            }
            return countOfEmptyBubbles;
        }
        private void CheckBubble(BubbleColorType color)
        {
            if (color == BubbleColorType.Empty) _emptyBubblesCount++;
            else _emptyBubblesCount--;

            if (_emptyBubblesCount >= _bubbles.Length) OnAllBubblesCleared?.Invoke();
        }
        private void CheckLastLineBubble(BubbleColorType color)
        {
            if (color == BubbleColorType.Empty) return;
            OnLastLineBubbleIsNotEmpty?.Invoke();
        }
        private void OnDisable()
        {
            foreach (var bubble in _lastLineOfBubbles)
            {
                bubble.OnBubbleColorChange += CheckLastLineBubble;
            }
            foreach (var bubble in _bubbles)
            {
                bubble.OnBubbleColorChange -= CheckBubble;
            }
        }
    }
}
