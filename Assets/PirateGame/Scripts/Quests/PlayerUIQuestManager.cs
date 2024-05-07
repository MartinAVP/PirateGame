using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIQuestManager : MonoBehaviour
{
    // otherScripts
    private PlayerUIManager ui;
    private PlayerQuestTracker quests;
    private List<QuestUI> uiQuests;

    [SerializeField] private GameObject questLogPrefab;

    private void Awake()
    {
        ui = GetComponent<PlayerUIManager>();
        quests = GetComponent<PlayerQuestTracker>();
    }

    public void updateUI()
    {
        foreach(Quest active in quests.availablequests)
        {
            GameObject currentQuest = Instantiate(questLogPrefab, ui.questsActiveContent);
            
            foreach(Transform questInfo in currentQuest.transform)
            {
                if(questInfo.name == "Quest Title")
                {
                    questInfo.GetComponent<TextMeshProUGUI>().text = active.questTitle;
                }

                if (questInfo.name == "Quest Description")
                {
                    questInfo.GetComponent<TextMeshProUGUI>().text = active.questDescription;
                }

                if (questInfo.name == "Quest Description")
                {
                    questInfo.GetComponent<TextMeshProUGUI>().text = active.questDescription;
                }
            }
        }
    }

    public struct QuestUI
    {
        public Transform title;
        public Transform description;
        public Transform progress;
    }
}
