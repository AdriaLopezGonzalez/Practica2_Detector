using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetector : MonoBehaviour
{
    [SerializeField]
    float DetectionRange = 3;

    [SerializeField]
    private float FieldOfView = 90;

    [SerializeField]
    LayerMask WhatIsVisible;

    List<Transform> _players;

    List<Transform> _playersDetected;

    public static Action<VisionDetector> OnPlayerDetected;
    public static Action<VisionDetector> OnPlayerUndetected;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        //upper FOV
        Vector2 start = transform.position;

        var direction = Quaternion.AngleAxis(
            FieldOfView / 2, transform.forward)
          * transform.right;
        Vector2 end = transform.position + direction.normalized * DetectionRange;
        Gizmos.DrawLine(start, end);

        //lower FOV
        start = transform.position;

        direction = Quaternion.AngleAxis(
            -FieldOfView / 2, transform.forward)
          * transform.right;
        end = transform.position + direction.normalized * DetectionRange;
        Gizmos.DrawLine(start, end);



        Gizmos.color = Color.white;
        
    }

   

    private void Awake()
    {
        _players = new List<Transform>();
        _playersDetected = new List<Transform>();
    }

    private void OnEnable()
    {
        PlayerTag.OnPlayerAdded += AddPlayer;
    }
    private void OnDisable()
    {
        PlayerTag.OnPlayerAdded -= AddPlayer;
    }

    private void AddPlayer(PlayerTag player)
    {
        _players.Add(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange())
        {
            if (IsInFOV())
            {
                if (IsNotBlocked())
                {
                    OnPlayerDetected?.Invoke(this);
                }
            }
        }
        if (!IsInRange() || !IsInFOV() || !IsNotBlocked())
        {
            OnPlayerUndetected?.Invoke(this);
        }

    }

   

    private bool IsNotBlocked()
    {
        for (int i = _playersDetected.Count - 1; i >= 0; i--)
        {
            if (IsBlocked(_playersDetected[i]))
                _playersDetected.RemoveAt(i);
        }
        return _playersDetected.Count > 0;
    }

    private bool IsBlocked(Transform player)
    {
        Vector2 v2 = player.position - transform.position;
        var hit = Physics2D.Raycast(transform.position, v2,DetectionRange, WhatIsVisible);
        return hit.transform != player;
    }


    private bool IsInFOV()
    {

        for (int i = _playersDetected.Count-1; i >= 0; i--)
        {
            float angle = GetAngle(_playersDetected[i]);
            if (angle > FieldOfView / 2f)
                _playersDetected.RemoveAt(i);
        }
        return _playersDetected.Count > 0;
    }

    private float GetAngle(Transform player)
    {
        Vector2 v1 = transform.right;
        Vector2 v2 = player.position - transform.position;
       return Vector2.Angle(v1, v2);
    }

    private bool IsInRange()
    {
        _playersDetected.Clear();
        foreach (var player in _players)
        {
            if (Vector2.Distance(player.position, transform.position) < DetectionRange)
                _playersDetected.Add(player);
        }
        return _playersDetected.Count > 0;
    }
}
