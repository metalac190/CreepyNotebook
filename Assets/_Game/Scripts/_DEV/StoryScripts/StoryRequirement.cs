using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev
{
    [System.Serializable]
    public class StoryRequirement
    {
        [Header("Requirements")]
        [SerializeField] StatIdentifier _requiredStat = StatIdentifier.None;
        public StatIdentifier RequiredStat => _requiredStat;

        [SerializeField] int _requiredStatLevel = 0;
        public int RequireStatLevel => _requiredStatLevel;

        [SerializeField] string _requiredKey = "";
        public string RequiredKey => _requiredKey;

        public bool RequirementsMet()
        {
            //TODO placeholder
            return true;
        }
    }
}

