using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
    public static Action<PlayerTag> OnPlayerAdded;
    // Start is called before the first frame update
    void Start()
    {
        OnPlayerAdded?.Invoke(this);
    }

    
}
