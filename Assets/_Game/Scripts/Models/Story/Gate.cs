using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gate
{
    [SerializeField] int _healthReq = 0;
    [SerializeField] int _sanityReq = 0;

    [SerializeField] int _calmReq = 0;
    [SerializeField] int _tenacityReq = 0;
    [SerializeField] int _perceptionReq = 0;
    [SerializeField] int _survivalReq = 0;
    // use this to check for keyitems in the player's inventory
    [SerializeField] int _requiredItemID = 0;

    public bool TestRequirements(PlayerStats stats, Inventory inventory)
    {
        // check stat requirements
        if(stats.Health.Value < _healthReq) { return false; }
        else if(stats.Sanity.Value < _sanityReq) { return false; }
        else if(stats.Calm.Value < _calmReq) { return false; }
        else if(stats.Tenacity.Value < _tenacityReq) { return false; }
        else if(stats.Perception.Value < _perceptionReq) { return false; }
        else if(stats.Survival.Value < _survivalReq) { return false; }
        // check item ID requirements
        bool locatedID = false;
        foreach(int iD in inventory.UIDs)
        {
            if(iD == _requiredItemID)
            {
                locatedID = true;
            }
        }
        if (locatedID == false)
            return false;

        // if made it here, it passed all the tests
        return true;
    }
}
