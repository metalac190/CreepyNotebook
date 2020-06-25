using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<int> UIDs { get; private set; } // these represent item numbers/quest collectibles

    private void Awake()
    {
        LoadInventory();
    }

    void LoadInventory()
    {
        //TODO
    }
}
