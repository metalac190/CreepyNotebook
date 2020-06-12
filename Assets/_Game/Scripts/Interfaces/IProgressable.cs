using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressable
{
    void Progress();
    bool CanProgress();
}
