using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlarm : MonoBehaviour
{
    
    SpriteRenderer _alarmRenderer;

    private void OnEnable()
    {
        VisionDetector.OnPlayerDetected += PlayerDetected;
        VisionDetector.OnPlayerUndetected += PlayerLeft;
    }
    private void OnDisable()
    {
        VisionDetector.OnPlayerDetected -= PlayerDetected;
        VisionDetector.OnPlayerUndetected -= PlayerLeft;
    }

    public void PlayerDetected(VisionDetector _visionDetector)
    {
        for (int i = 0; i < _visionDetector.transform.childCount; i++)
        {
            if (_visionDetector.transform.GetChild(i).TryGetComponent<EnemyAlarm>(out EnemyAlarm ownAlarm))
            {
                ownAlarm.ChangeColor(Color.red);
            }
        }
    }

    public void PlayerLeft(VisionDetector _visionDetector)
    {
        for (int i = 0; i < _visionDetector.transform.childCount; i++)
        {
            if (_visionDetector.transform.GetChild(i).TryGetComponent<EnemyAlarm>(out EnemyAlarm ownAlarm))
            {
                ownAlarm.ChangeColor(new Color(0, 0, 0, 0));
            }
        }
    }

    private void ChangeColor(Color color)
    {
        if (_alarmRenderer == null)
            _alarmRenderer=GetComponent<SpriteRenderer>();

        _alarmRenderer.color = color;
    }
}
