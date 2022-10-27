using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bubbles.BubbleGun
{
    public interface IBubblesGenerator
    {
        BubbleColorType GenerateBubble();
    }
}
