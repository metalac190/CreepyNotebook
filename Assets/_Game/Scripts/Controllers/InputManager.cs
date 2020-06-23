using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public event Action Clicked = delegate { };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Clicked.Invoke();
        }
    }
}
