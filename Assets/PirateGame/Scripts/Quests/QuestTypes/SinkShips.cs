using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SinkShips", menuName = "Quests/SinkShips")]
public class SinkShips : Quest
{
    public int shipsToSink;
    public int shipsSunk;
    public string shipTag;
}
