using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action OnClicked = delegate { };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnClicked.Invoke();
        }
    }
}
