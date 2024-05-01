using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Quests/GatherQuest")]
public class GatherQuest : Quest
{
    [Header("Gathering Settings")]
    public int Quantity;
    public ItemType itemType;
}
