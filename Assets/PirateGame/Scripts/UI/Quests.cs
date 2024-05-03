using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*public class Quests : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string Name;
        public string Description;
    }

    [Header("Info")] public Info information;

    [System.Serializable]
    public struct Stat
    {
        public int Currency;
        public int XP;

        public Stat(int Currency, int XP)
        {
            this.Currency = Currency;
            this.XP = XP;
        }
    }

    [Header("Reward")] public Stat Reward = new Stat(10, 5);

    public bool Completed {  get; protected set; }
    public QuestCompletedEvent CompletedEvent;

    public abstract class QuestGoal: ScriptableObject
    {
        protected string Description;
        public int CurrentAmount { get; protected set; }
        public int RequieredAmount = 1;

        public bool Completed { get; protected set; }
        [HideInInspector]public UnityEvent GoalCompleted;

        public virtual string GetDescription()
        {
            return Description;
        }

        public virtual void Initialize()
        {
            Completed = false;
            GoalCompleted = new UnityEvent();
        }

        protected void Evaluate()
        {
            if(CurrentAmount >= RequieredAmount)
            {
                Complete();
            }
        }

        private void Complete()
        {
            Completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }

        public void Skip()
        {
            Complete();
        }
    }

    public List<QuestGoal> Goals;

    public void Initialize()
    {
        Completed = false;
        QuestCompleted = new QuestCompletedEvent();

        foreach(var goal in Goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {

    }
}

public class QuestCompletedEvent : UnityEvent<Quests> { };*/

/*[System.Serializable]
struct Quest
{
    // Identifier
    public int id;

    // Information
    public string questTitle;
    public string questDescription;

    // Quest Technicalities
    public QuestType type;
    
    // Technical Values
    public bool questInProgress;
    public bool questNotStarted;
    public bool questCompleted;
}

public enum QuestType
{
    GatherItems,
    EnterZone,
    ShootTarget
}*/