using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gate
{
    // player stats
    [SerializeField] int _calmReq = 0;
    [SerializeField] int _survivalReq = 0;
    [SerializeField] int _tenacityReq = 0;
    // use this to check for keyitems in the player's inventory
    [SerializeField] int _requiredItemID = 0;

    public bool TestRequirements(PlayerStats stats, Inventory inventory)
    {
        // check stat requirements
        if (TestStatRequirements(stats) == false)
        {
            return false;
        }
        
        if(TestInventoryRequirements(inventory) == false)
        {
            return false;
        }
        
        // if made it here, it passed all the tests
        return true;
    }

    bool TestStatRequirements(PlayerStats stats)
    {
        // if any of our stats don't meet the minimum, we did not pass
        if (stats.Calm.Value < _calmReq) { return false; }
        else if (stats.Survival.Value < _survivalReq) { return false; }
        else if (stats.Tenacity.Value < _tenacityReq) { return false; }
        // otherwise we pass the requirement!
        else
        {
            return true;
        }
    }

    bool TestInventoryRequirements(Inventory inventory)
    {
        // if it's 0, we haven't specified anything, so let it pass
        if(_requiredItemID == 0)
        {
            return true;
        }
        // check each of our inventory ids, to see if we have it
        foreach (int iD in inventory.UIDs)
        {
            if (iD == _requiredItemID)
            {
                return true;
            }
        }

        return false;
    }
}
