using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class PlayerQuestTracker : MonoBehaviour
{
    public List<Quest> availablequests;
    private List<Quest> startedQuests;
    private List<Quest> completedQuests;

    private PlayerInventoryManager inventoryManager;
    private PlayerUIQuestManager playerUIQuestManager;
    private PlayerAnnouncementManager playerAnnouncementManager;

    public UnityEvent newQuest;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventsType.SHIPSUNK, addShipsSunk);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventsType.SHIPSUNK, addShipsSunk);
    }

    // Global Quests Variable
    public int shipsSunk;

    private void Awake()
    {
        inventoryManager = GetComponent<PlayerInventoryManager>();
        playerUIQuestManager = GetComponent<PlayerUIQuestManager>();
        playerAnnouncementManager = GetComponent<PlayerAnnouncementManager>();

        //availablequests = new List<Quest>();
        completedQuests = new List<Quest>();
        startedQuests = new List<Quest>();

    }

    private void Start()
    {
        newQuest.AddListener(this.transform.GetComponent<PlayerUIQuestManager>().updateUI);
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
                quest.name = quest.questTitle;

                if (availablequests[j] is EnterAreaQuest enterArea)
                {
                    foreach(var area in enterArea.areaList)
                    {
                        area.areaReached = false;
                    }
                }

                j++;
            }
        }
    }

    // UI Updater Methods
    #region UI Updater Methods
    public int inventoryUpdate(Quest quest)
    {
        List<InventoryItem> playerInventory = inventoryManager.GetInventory();

        if (quest is GatherQuest gatherQuest)
        {
            // Loop player inventory
            foreach (InventoryItem item in playerInventory)
            {
                // Check if the item type in the inventory is the same as the item type requiered in the quest
                if (item.type == gatherQuest.itemType)
                {
                    // Check the quantity of the item in the inventory and check if its greater than or equal to the requierements.
                    return item.quantity;
                }
            }
            // The quest is not a gather quest
        }

        return 0;
    }

    public int checkAreasReached(Quest quest)
    {
        int areasComplete = 0;
        if(quest is EnterAreaQuest enterArea)
        {
            // Make a new List
            List<bool> areasReached = new List<bool>();
            // Copy the values of the reachable into a new Local List
            foreach (var area in enterArea.areaList)
            {
                areasReached.Add(area.areaReached);
            }

            for (int i = 0; i < areasReached.Count; i++)
            {
                if (areasReached[i] == true)
                    areasComplete++;
            }
        }

        return areasComplete;
    }

    public int getTotalAreasToReach(Quest quest)
    {
        int totalAreasToReach = 0;
        if (quest is EnterAreaQuest enterArea)
        {
            foreach (var area in enterArea.areaList)
            {
                totalAreasToReach++;
            }
        }
        return totalAreasToReach;
    }
    #endregion

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
                                //Debug.Log("Enough items found! ( " + item.quantity + " / " + gatherQuest.Quantity + " )");
                                //Debug.Log("The quest has been completed");
                                CompleteQuest(startedQuests[i]);
                            }
                            else
                            {
                                //Player does not have enough items to complete the quest.
                                //Debug.Log("Not enough items yet ( " + item.quantity + " / " + gatherQuest.Quantity + " )");
                            }
                            break;
                            // The player does not have any item type requiered by the quest
                        }
                    }
                    // The quest is not a gather quest
                    GetComponent<PlayerUIQuestManager>().updateUI();
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
                    // Check if the zone is a Reachable Area
                    if (zone.tag == "ReachArea")
                    {
                        // Loop through each area in the quest tasked
                        foreach (var area in enterArea.areaList)
                        {

                            //print("Iteration");
                            // Check if the Name of the Zone matches the Name of the Quest
                            if (area.areaName == zone.name)
                            {
                                //print("The area has been reached: " + zone.name);
                                area.areaReached = true;
                                //print("The area " + area.areaName + " has been marked as " + area.areaReached);
                            }
                        }

                        // Make a new List
                        List<bool> areasReached = new List<bool>();
                        // Copy the values of the reachable into a new Local List
                        foreach(var area in enterArea.areaList)
                        {
                            areasReached.Add(area.areaReached);
                        }

                        // Check if all the values are true
                        bool allTrue = areasReached.All(b => b);

                        // If all the areas have been completed, then complete the quest
                        if (allTrue == true)
                        {
                            //Debug.Log("All Areas have been reached: " + allTrue);
                            CompleteQuest(startedQuests[i]);
                        }


                    }
                }
            }
            GetComponent<PlayerUIQuestManager>().updateUI();
        }
    }

    private void addShipsSunk()
    {
        shipsSunk++;
        checkShipsSunk();
    }

    public void checkShipsSunk()
    {
        if (startedQuests.Count != 0)
        {
            // Loop through all active quests
            for (int i = 0; i < startedQuests.Count; i++)
            {
                // Check if the quest is a Gather quest
                if (startedQuests[i] is SinkShips sinkShips)
                {
                    // Check the quantity of the item in the inventory and check if its greater than or equal to the requierements.
                    if (shipsSunk >= sinkShips.shipsToSink)
                    {
                        // Player has enough items for completed quest
                        CompleteQuest(startedQuests[i]);
                        shipsSunk -= sinkShips.shipsToSink;
                    }
                    else
                    {
                        //Player does not have enough items to complete the quest.
                    }
                    break;
                    // The player does not have any item type requiered by the quest
                    // The quest is not a gather quest
                }
            }
            GetComponent<PlayerUIQuestManager>().updateUI();
        }
    }

    public void CompleteQuest(Quest quest)
    {
        quest.status = QuestStatus.completed;
        completedQuests.Add(quest);
        startedQuests.Remove(quest);

        GetComponent<PlayerUIQuestManager>().completeQuest(quest);
        playerAnnouncementManager.addAnnouncementQueued(quest.questTitle, "Completed", PlayerAnnouncementManager.questBannerType.MerchantAlliance, quest);
    }

/*    private void OnGUI()
    {
        if(GUILayout.Button("Start Gathering Quest"))
        {
            startQuest(0);
            startQuest(1);
            startQuest(2);
        }
    }*/

    public void startQuest(int id)
    {
        availablequests[id].status = QuestStatus.inProgress;
        startedQuests.Add(availablequests[id]);
        GetComponent<PlayerUIQuestManager>().newQuest(availablequests[id]);
        //newQuest.Invoke();
        checkInventory();

        playerAnnouncementManager.addAnnouncementQueued(availablequests[id].questTitle, availablequests[id].questDescription, PlayerAnnouncementManager.questBannerType.MerchantAlliance, availablequests[id]);
    }
}

public enum QuestStatus
{
    notStarted,
    inProgress,
    completed
}