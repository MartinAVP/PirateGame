using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    [SerializeField] List<Quest> quests;

    [System.Serializable]
    struct Quest
    {
        public int id;
        public string questTitle;
        public string questDescription;
        public bool questInProgress;
        public bool questNotStarted;
        public bool questCompleted;

        //Constructor (not necessary, but helpful)
/*        public Quest(int id, string questTitle,string questDescription, bool questInProgress, bool questNotStarted, bool questCompleted)
        {
            this.id = id;
            this.questTitle = questTitle;
            this.questDescription = questDescription;
            this.questInProgress = questInProgress;
            this.questNotStarted = questNotStarted;
            this.questCompleted = questCompleted;
        }*/
    }
}
