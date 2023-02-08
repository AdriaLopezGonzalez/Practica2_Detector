using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseDetector : MonoBehaviour
{
    [SerializeField]
    float DetectionRange = 2;

    PlayerMovement _playerMovement; //

    List<Transform> _players;

    List<Transform> _playersDetected;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.white;
    }

    private void Awake()
    {
        _players = new List<Transform>();
        _playersDetected = new List<Transform>();
        //_playerMovement = gameObject.GetComponent<PlayerMovement>();
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

    void Update()
    {
        if (IsInRange())
        {
            if(IsMoving())
            {
                Debug.Log("ss");
            }
        }
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

    private bool IsMoving()
    {
        _playersDetected.Clear();
        foreach (var player in _players)
        {
            if (player.gameObject.GetComponent<PlayerMovement>().IsMoving)
                _playersDetected.Add(player);
        }
        return _playersDetected.Count > 0;
    }
}
