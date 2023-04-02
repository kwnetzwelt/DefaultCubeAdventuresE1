using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayerInput : MonoBehaviour
{
    public bool PlayerInputEnabled { get; private set; }

    private void OnEnable()
    {
        PlayerInputEnabled = true;
    }

    public void DisablePlayerInput()
    {
        PlayerInputEnabled = false;
    }
}
