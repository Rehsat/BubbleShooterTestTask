using System;
using UnityEngine;

public class RandomBubbleGenerator : MonoBehaviour, IBubblesGenerator
{
    public BubbleColorType GenerateBubble()
    {
        var bubblesEnumLength = Enum.GetValues(typeof(BubbleColorType)).Length;
        BubbleColorType randomBubbleColor = (BubbleColorType)UnityEngine.Random.Range(1, bubblesEnumLength);
        return randomBubbleColor;
    }
}
