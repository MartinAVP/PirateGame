using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerUIQuestManager;

public class PlayerUIQuestManager : MonoBehaviour
{
    // otherScripts
    private PlayerUIManager ui;
    private PlayerQuestTracker quests;
    [SerializeField] private List<QuestUI> activeQuests;
    [SerializeField] private List<QuestUI> completedQuests;

    [SerializeField] private GameObject questLogPrefab;

    private void Awake()
    {
        ui = GetComponent<PlayerUIManager>();
        quests = GetComponent<PlayerQuestTracker>();
    }

    public void updateUI()
    {
        foreach(QuestUI active in activeQuests)
        {
            active.title.GetComponent<TextMeshProUGUI>().text = active.quest.questTitle;
            active.description.GetComponent<TextMeshProUGUI>().text = active.quest.questDescription;

            if (active.quest is GatherQuest gatherQuest)
            {
                //print("Quest is Gather");
                active.progress.GetComponent<TextMeshProUGUI>().text = quests.inventoryUpdate(active.quest).ToString() + " / " + gatherQuest.Quantity.ToString();
            }

            if (active.quest is EnterAreaQuest enterArea)
            {
                active.progress.GetComponent<TextMeshProUGUI>().text = quests.checkAreasReached(active.quest).ToString() + " / " + quests.getTotalAreasToReach(active.quest).ToString();
            }

            if (active.quest is SinkShips shipsSinking)
            {
                active.progress.GetComponent<TextMeshProUGUI>().text = quests.shipsSunk + " / " + shipsSinking.shipsToSink.ToString();
            }

        }
    }

    public void newQuest(Quest quest)
    {
        GameObject currentQuest = Instantiate(questLogPrefab, ui.questsActiveContent);
        currentQuest.GetComponent<QuestContainer>().quest = quest;

        Transform tempTitle = null;
        Transform tempDesc = null;
        Transform tempProgress = null;

        foreach (Transform questInfo in currentQuest.transform)
        {
            if (questInfo.name == "Quest Title")
                tempTitle = questInfo;

            if (questInfo.name == "Quest Description")
                tempDesc = questInfo;

            if (questInfo.name == "Quest Progress")
                tempProgress = questInfo;

        }

        activeQuests.Add(new QuestUI(quest, tempTitle, tempDesc, tempProgress, currentQuest));

        updateUI();
    }

    public void completeQuest(Quest quest)
    {
        // Find where the quest is stored
        int targetPos = -1;
        int i = 0;
        foreach(QuestUI questUI in activeQuests)
        {
            if (questUI.quest == quest)
                targetPos = i;
            i++;
        }
        print("Quest found at :" + targetPos);

        // Check if the quest is not in the UI.
        if(targetPos == -1)
        {
            return;
        }

        // Add it to the completedQuests list
        QuestUI tempSlot;
        tempSlot = activeQuests[targetPos];
        tempSlot.card.transform.parent = ui.questsCompletedContent.transform;
        tempSlot.progress.GetComponent<TextMeshProUGUI>().text = "Completed";

        completedQuests.Add(tempSlot);

        // Remove that quest from the activeQuest List
        activeQuests.RemoveAt(targetPos);
        updateUI();

    }

    [System.Serializable]
    public class QuestUI
    {
        public Quest quest;
        public GameObject card;
        public Transform title;
        public Transform description;
        public Transform progress;

        public QuestUI(Quest quest, Transform title, Transform description, Transform progress, GameObject card)
        {
            this.quest = quest;
            this.title = title;
            this.description = description;
            this.progress = progress;
            this.card = card;
        }
    }
}
