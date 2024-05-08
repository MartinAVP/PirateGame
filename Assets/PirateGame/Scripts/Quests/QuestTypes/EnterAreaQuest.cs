using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnterAreaQuests", menuName = "Quests/EnterArea")]
public class EnterAreaQuest : Quest
{
    [Header("Area Settings")]
    public List<AreaInformation> areaList;
    //public ItemType itemType;

    public void SetReached(AreaInformation info, bool value)
    {
        info.areaReached = value;
    }

    [System.Serializable]
    public class AreaInformation
    {
        public bool areaReached;

        [Tooltip("The area name must match the gameObject name in unity")]
        public string areaName;

        public void MarkAsReached()
        {
            areaReached = true;
        }
    }
}
