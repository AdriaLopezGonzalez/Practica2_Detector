using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Patrolling : MonoBehaviour
{

    WallDetector _wallDetector;

    [SerializeField]
    float Speed = 5;

    private void Awake()
    {
        _wallDetector = GetComponentInChildren<WallDetector>();
    }
    // Update is called once per frame
    void Update()
    {
        if (WallDetected())
            Flip();
        Move();
    }

    private bool WallDetected() 
    {     
        return _wallDetector.HasWall;
    }

    private void Flip()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(90, 270)));
    }

    private void Move()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
       // transform.Translate(Vector2.right * Speed * Time.deltaTime, Space.Self);
    }
}
