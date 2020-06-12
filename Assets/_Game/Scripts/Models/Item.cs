using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Data/Item")]
public class Item : ScriptableObject
{
    [SerializeField] int _uID = 000;
    public int UID => _uID;

    [SerializeField] string _name = "...item...";
    public string Name => _name;
}
