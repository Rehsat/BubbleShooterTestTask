using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerTouchDetector))]
public class BubbleGunAim : MonoBehaviour
{
    private PlayerTouchDetector _detector;
    private void Awake()
    {
        _detector = GetComponent<PlayerTouchDetector>();
    }
    private void OnEnable()
    {
        
    }
}
