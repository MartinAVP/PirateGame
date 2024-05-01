using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestTracker : MonoBehaviour
{
    public Quest currentQuest;

    public List<Quest> availablequests;
    public List<Quest> startedQuests;
    public List<Quest> completedQuests;

    private PlayerInventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GetComponent<PlayerInventoryManager>();
    }

    private void Start()
    {
        if(availablequests.Count == 0)
        {
            Debug.LogWarning("Player has no quests assigned");
            return;
        }

        int j = 0;
        foreach (Quest quest in availablequests)
        {
            quest.id = j;
            quest.status = QuestStatus.notStarted;
        }

        currentQuest = availablequests[0];
    }

    public void checkInventory()
    {
        List<InventoryItem> playerInventory = inventoryManager.GetInventory();
        //Debug.Log("Quest system is checking inventories");
        GatherQuest gatherItems;
        // No Quests started
        if(startedQuests.Count != 0)
        {
            // Loop through all active quests
            for(int i = 0; i < startedQuests.Count; i++)
            {
                // Check if the quest is a Gather quest
                if (startedQuests[i] is GatherQuest gatherQuest)
                {
                    // Loop player inventory
                    foreach (InventoryItem item in playerInventory)
                    {
                        // Check if the item type in the inventory is the same as the item type requiered in the quest
                        if(item.type == gatherQuest.itemType)
                        {
                            // Check the quantity of the item in the inventory and check if its greater than or equal to the requierements.
                            if (item.quantity >= gatherQuest.Quantity)
                            {
                                // Player has enough items for completed quest
                                Debug.Log("Enough items found! ( " + item.quantity + " / " + gatherQuest.Quantity + " )");
                                Debug.Log("The quest has been completed");
                                CompleteQuest(startedQuests[i]);
                            }
                            else
                            {
                                //Player does not have enough items to complete the quest.
                                Debug.Log("Not enough items yet ( " + item.quantity + " / " + gatherQuest.Quantity + " )");
                            }
                            break;
                            // The player does not have any item type requiered by the quest
                        }
                    }
                // The quest is not a gather quest
                }
            }
        }
    }

    public void CompleteQuest(Quest quest)
    {
        quest.status = QuestStatus.completed;
        completedQuests.Add(quest);
        startedQuests.Remove(quest);

    }

    public void updateGatheringQuest(Quest quest, int quantity)
    {

    }

/*    private void OnGUI()
    {
        if(GUILayout.Button("Tomate"))
        {
            startQuest(0);
        }
    }*/

    public void startQuest(int id)
    {
        availablequests[0].status = QuestStatus.inProgress;
        startedQuests.Add(availablequests[id]);
        checkInventory();
    }
}

public enum QuestStatus
{
    notStarted,
    inProgress,
    completed
}