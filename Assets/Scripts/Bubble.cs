using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Bubble : MonoBehaviour
{

    [SerializeField] protected LayerMask BubblesLayer;
    [SerializeField] private BubbleColorType _myBubbleColor;
    [SerializeField] private float _neighboursDetectRadius;
    public BubbleColorType BubbleColor
    {
        get => _myBubbleColor;
        set
        {
            _myBubbleColor = value;
            OnBubbleColorChange?.Invoke(_myBubbleColor);
            VisualisateMyColorByType();
        }
    }
    public bool IsActive { get; set; }
    public List<Bubble> Neighbours;

    private SpriteRenderer mySpriteRenderer;
    private Collider2D myCollider;

    public Action<BubbleColorType> OnBubbleColorChange;
    private void Awake()
    {
        Neighbours = new List<Bubble>(10);
    }
    private void OnEnable()
    {
        if(mySpriteRenderer == null)
        {
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            myCollider = GetComponent<Collider2D>();
        }
        VisualisateMyColorByType();
        IsActive = true;
      //  DetectNeighbours();
    }
    private void Start()
    {

        DetectNeighbours();
    }
    private void VisualisateMyColorByType()
    {
        mySpriteRenderer.enabled = true;
        myCollider.isTrigger = false;
        switch (BubbleColor)
        {
            case BubbleColorType.Empty:
                mySpriteRenderer.enabled = false;
                myCollider.isTrigger = true;
                break;
            case BubbleColorType.Blue:
                mySpriteRenderer.color = Color.blue;
                break;
            case BubbleColorType.Green:
                mySpriteRenderer.color = Color.green;
                break;
            case BubbleColorType.Red:
                mySpriteRenderer.color = Color.red;
                break;
        }

    }
    public Bubble[] GetNeighborsWithColor(BubbleColorType color)
    {
        List<Bubble> bubbleResults = new List<Bubble>(5);
        foreach (var neighbour in Neighbours)
        {
            if (neighbour == null) continue;
            var resultBubble = neighbour.GetComponent<Bubble>();
            if (resultBubble == null || resultBubble == this || resultBubble.BubbleColor != color) continue;
            bubbleResults.Add(resultBubble);
        }

        return bubbleResults.ToArray();
    }
    protected void DetectNeighbours()
    {
        Neighbours.Clear();
        Collider2D[] results = new Collider2D[10];
        Physics2D.OverlapCircleNonAlloc
            (transform.position,
            _neighboursDetectRadius,
            results,
            BubblesLayer);

        foreach(var result in results)
        {
            if (result == null) continue;
            var resultBubble = result.GetComponent<Bubble>();
            if (resultBubble == null || resultBubble == this) continue;
            Neighbours.Add(resultBubble);
        }
    }
    public void Burst()
    {
        IsActive = false;
        if (Neighbours.Count > 0)
        {
            TryBurstNeighbours();
        }
        BubbleColor = BubbleColorType.Empty;
    }
    private void TryBurstNeighbours()
    {
        foreach (var neighbour in Neighbours)
        {
            if(neighbour == null || neighbour.IsActive == false)
            {
                continue;
            }
            if (neighbour.BubbleColor == BubbleColor)
            {
                neighbour.Burst();
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.gray;
        Handles.DrawSolidDisc(transform.position, Vector3.forward, _neighboursDetectRadius);
    }
    private void OnValidate()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
        VisualisateMyColorByType();
    }
#endif
}
public enum BubbleColorType
{
    Empty,
    Red,
    Green,
    Blue
}
