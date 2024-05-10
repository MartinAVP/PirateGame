using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] public Transform playerCanvas;

    // NOTE: The Canvas Names are very important

    // Panels
    [HideInInspector] public Transform mainLayout;
    [HideInInspector] public Transform playerWheel;
    [HideInInspector] public Transform containerInventory;
    [HideInInspector] public Transform quests;
    [HideInInspector] public Transform announcements;
    [HideInInspector] public Transform blackScreen;

    // Money Displayer
    [HideInInspector] public Transform mainMoneyLog;
    [HideInInspector] public Transform mainMoneyLogContent;

    // Container Positions
    [HideInInspector] public Transform containerPlayerInventorySlots;
    [HideInInspector] public Transform containerInventorySlots;

    // Player Wheel Positions
    [HideInInspector] public Transform playerWheelSlots;

    // Quests
    [HideInInspector] public Transform questsActive;
    [HideInInspector] public Transform questsCompleted;

    [HideInInspector] public Transform questsActiveContent;
    [HideInInspector] public Transform questsCompletedContent;

    [HideInInspector]public UIContainerSlot[] containerSlots;

    // Announcements
    [HideInInspector] public Transform announcementsTopBar;
    [HideInInspector] public Transform announcementsBottomBar;

    [HideInInspector] public Transform announcementTopText;
    [HideInInspector] public Transform announcementBottomText;

    private void Awake()
    {
        containerSlots = new UIContainerSlot[16];
    }

    private void OnEnable()
    {
        // Find the Panels
        foreach (Transform canvasChild in playerCanvas)
        {
            if (canvasChild.name == "MainLayout")
                mainLayout = canvasChild;
            else if (canvasChild.name == "PlayerWheel")
                playerWheel = canvasChild;
            else if(canvasChild.name == "PlayerContainerInventory")
                containerInventory = canvasChild;
            else if (canvasChild.name == "PlayerQuestLog")
                quests = canvasChild;
            else if (canvasChild.name == "Announcement")
                announcements = canvasChild;
            else if (canvasChild.name == "BlackScreen")
                blackScreen = canvasChild;
        }

        // Find Main Layout Positions
        foreach (Transform mainChildren in mainLayout)
        {
            if (mainChildren.name == "MoneyDisplayer")
            {
                mainMoneyLog = mainChildren;
                mainMoneyLogContent = mainChildren.GetChild(0).GetChild(0);
            }
                
        }

        // Find the Container Positions
        foreach (Transform containerInventoryChild in containerInventory)
        {
            if(containerInventoryChild.name == "PlayerInventorySlots")
                containerPlayerInventorySlots = containerInventoryChild;
            else if(containerInventoryChild.name == "ContainerInventorySlots")
                containerInventorySlots = containerInventoryChild;
        }

        // Find the PlayerWheel Positions
        foreach (Transform playerWheelChild in playerWheel)
        {
            if (playerWheelChild.name == "PlayerQuickInventory")
                playerWheelSlots = playerWheelChild;
        }

        // Find the Quests Positions
        foreach (Transform questsChild in quests)
        {
            if (questsChild.name == "ActiveQuests")
            {
                questsActive = questsChild;

                questsActiveContent = questsActive.GetChild(0).GetChild(0);
            }

            if (questsChild.name == "CompletedQuest")
            {
                questsCompleted = questsChild;

                questsCompletedContent = questsCompleted.GetChild(0).GetChild(0);
            }
        }

        // Find Announcement Positions
        foreach(Transform announcements in announcements)
        {
            if(announcements.name == "TopPart")
            {
                announcementsTopBar = announcements.GetChild(0);
                announcementTopText = announcements.GetChild(0).GetChild(0);
            }

            if (announcements.name == "BottomPart")
            {
                announcementsBottomBar = announcements.GetChild(0);
                announcementBottomText = announcements.GetChild(0).GetChild(0);
            }
        }
    }

    // Inventory Wheel Public Functions
    public void openInventoryWheel()
    {
        playerWheel.gameObject.SetActive(true);
        EnableCursor();
    }

    public void closeInventoryWheel()
    {
        playerWheel.gameObject.SetActive(false);
        DisableCursor();
    }

    public void openQuestLog()
    {
        quests.gameObject.SetActive(true);
        EnableCursor();
    }

    public void closeQuestLog()
    {
        quests.gameObject.SetActive(false);
        DisableCursor();
    }

    // Container Inventory Public Functions
    public void openContainerInventory()
    {
        containerInventory.gameObject.SetActive(true);
        EnableCursor();
    }

    public void closeContainerInventory()
    {
        containerInventory.gameObject.SetActive(false);
        DisableCursor();
    }

    public void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
