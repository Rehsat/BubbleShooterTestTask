using UnityEngine;

[RequireComponent(typeof(IBubblesGenerator))]
public class Field : MonoBehaviour
{
    [SerializeField] private bool _generateOnStart;

    [SerializeField] private Bubble[] _bubbles;
    private IBubblesGenerator _bubblesGenerator;
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
    }
}
