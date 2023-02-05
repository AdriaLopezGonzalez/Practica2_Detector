using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    public bool HasWall => _hasWall;
    private bool _hasWall;

  
    [SerializeField]
    float DetectionDistance = 1.5f;

    [SerializeField]
    LayerMask WhatIsNotWall;
    // Update is called once per frame
    void Update()
    {
        DetectWall();
    }

    private void DetectWall()
    {
        var hit = Physics2D.Raycast(transform.position, 
            Vector2.right, DetectionDistance, WhatIsNotWall);

        _hasWall = ( hit.collider == null);
        Debug.Log(HasWall);
    }
}
