using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GatherQuests", menuName = "Quests/GatherQuest")]
public class GatherQuest : Quest
{
    [Header("Gathering Settings")]
    public int Quantity;
    public ItemType itemType;
}

[CreateAssetMenu(fileName = "EnterAreaQuests", menuName = "Quests/EnterArea")]
public class EnterAreaQuest : Quest
{
    [Header("Area Settings")]
    public List<AreaInformation> areaList;
    //public ItemType itemType;

    [System.Serializable]
    public struct AreaInformation
    {
        public bool AreaReached;
        [Tooltip("The area name must match the gameObject name in unity")]
        public string AreaName;

        public void SetReached(bool reached)
        {
            AreaReached = reached;
        }

        public void MarkAsReached()
        {
            AreaReached = true;
        }
    }
}
