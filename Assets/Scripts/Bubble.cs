using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bubble : MonoBehaviour
{

    [SerializeField] protected LayerMask BubblesLayer;
    [SerializeField] protected BubbleColorType MyBubbleColor;
    [SerializeField] private float _neighboursDetectRadius;
    public BubbleColorType BubbleColor => MyBubbleColor;
    public List<Bubble> Neighbours;

    private SpriteRenderer mySpriteRenderer;
    public bool IsActive { get; set; }
    private void Awake()
    {
        Neighbours = new List<Bubble>(10);
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
    }
    public Bubble(BubbleColorType bubbleColor)
    {
        MyBubbleColor = bubbleColor;
    }
    private void OnEnable()
    {
        VisualisateMyColorByType();
        IsActive = true;
        try
        {
            FindNeighbours();
        }
        catch
        {
            FindNeighbours();
        }
    }
    private void VisualisateMyColorByType()
    {
        switch (BubbleColor)
        {
            case BubbleColorType.None:
                gameObject.SetActive(false);
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
    protected void FindNeighbours()
    {
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
        gameObject.SetActive(false);
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
        gameObject.SetActive(true);
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //VisualisateMyColorByType();
    }
#endif
}
public enum BubbleColorType
{
    None,
    Red,
    Green,
    Blue
}
