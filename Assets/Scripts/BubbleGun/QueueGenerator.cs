using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bubbles.BubbleGun
{
    public class QueueGenerator : MonoBehaviour, IBubblesGenerator
    {
        [SerializeField] private bool _isCycle;
        [SerializeField] private BubbleColorType[] _queue;
        private int _currentBubbleId;

        public BubbleColorType GenerateBubble()
        {
            var generatedBubble = _queue[_currentBubbleId];

            _currentBubbleId++;
            if (_currentBubbleId >= _queue.Length)
            {
                if (_isCycle)
                {
                    _currentBubbleId = 0;
                }
                else
                {
                    return BubbleColorType.Empty;
                }
            }
            return generatedBubble;
        }
    }
}