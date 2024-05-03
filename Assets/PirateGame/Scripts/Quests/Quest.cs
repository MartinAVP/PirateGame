using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : ScriptableObject
{
    [Header("Quest Information")]
    // Information
    public string questTitle;
    public string questDescription;

    // Identifier
    [HideInInspector]public int id;

    public QuestStatus status;
}