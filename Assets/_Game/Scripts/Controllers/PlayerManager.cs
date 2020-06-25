using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerStats _stats = null;
    public PlayerStats Stats => _stats;

    [SerializeField] Inventory _inventory = null;
    public Inventory Inventory => _inventory;
}
