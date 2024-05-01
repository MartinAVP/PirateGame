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

    public void OnDisable()
    {
        startedQuests.Clear();
        completedQuests.Clear();
        //PlayerQuestTracker = null;
    }

    private void Awake()
    {
        inventoryManager = GetComponent<PlayerInventoryManager>();

        //availablequests = new List<Quest>();
        completedQuests = new List<Quest>();
        startedQuests = new List<Quest>();
    }

/*    public int availableQuestNum;
    public int completedQuestNum;
    public int startedQuestsNum;

    private void Update()
    {
        availableQuestNum = availablequests.Count;
        completedQuestNum = completedQuests.Count;
        startedQuestsNum = startedQuests.Count;
    }*/


    private void Start()
    {
        if(availablequests.Count == 0)
        {
            Debug.LogWarning("Player has no quests assigned");
            return;
        }
        else
        {
            int j = 0;
            foreach (Quest quest in availablequests)
            {
                quest.id = j;
                quest.status = QuestStatus.notStarted;
            }

            currentQuest = availablequests[0];
        }
    }

    public void checkInventory()
    {
        List<InventoryItem> playerInventory = inventoryManager.GetInventory();
        //Debug.Log("Quest system is checking inventories");
        //GatherQuest gatherItems;
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

    public void checkEnteredZone(GameObject zone)
    {
        // Check if there is no quests on the List
        if(startedQuests.Count != 0)
        {
            // Loop through all active quests
            for (int i = 0; i < startedQuests.Count; i++)
            {
                // Check if the quest type is Entering an AreaQuest
                if (startedQuests[i] is EnterAreaQuest enterArea)
                {
                    bool allAreasCompleted = true;
                    // Loop through each area in the quest tasked
                    foreach (var area in enterArea.areaList)
                    {
                        // Check if the zone is a Reachable Area
                        if(zone.tag == "ReachArea")
                        {
                            print("Iteration");
                            // Check if the Name of the Zone matches the Name of the Quest
                            if(area.AreaName == zone.name)
                            {
                                print("The area has been reached: " + zone.name);
                                area.SetReached(true);
                                area.MarkAsReached();
                                print("The area " + zone.name + " has been marked as " + area.AreaReached);
                                // Check if the area Reached is False
                                if (area.AreaReached == false)
                                {
                                    allAreasCompleted = false;
                                }
                            }
                        }
                    }


                    // If all the areas have been completed, then complete the quest
                    if (allAreasCompleted == true)
                    {
                        Debug.Log("All Areas have been reached: " + allAreasCompleted);
                        CompleteQuest(startedQuests[i]);
                    }
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

    private void OnGUI()
    {
        if(GUILayout.Button("Start Gathering Quest"))
        {
            startQuest(0);
        }

        if (GUILayout.Button("Start LookAround"))
        {
            startQuest(0);
        }

    }

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