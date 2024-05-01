using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaEntering : MonoBehaviour
{
    UnityEvent enteredArea;
    private PlayerQuestTracker questTracker;

    private void Awake()
    {
        questTracker = FindFirstObjectByType<PlayerQuestTracker>();
        //enteredArea.AddListener(questTracker.checkEnteredZone);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Entered Area");
        questTracker.checkEnteredZone(this.gameObject);
        //enteredArea.Invoke();
    }
}
