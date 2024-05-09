using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAnnouncementManager : MonoBehaviour
{
    public PlayerUIManager ui;
    public PlayerQuestTracker quests;
    [SerializeField] private List<Announcement> announcementsQueued;

    private Animator animator;
    private AudioSource audioSource;

    private bool isAnnouncing = false;

    [Header("Audio Clips")]

    // Merchant Alliance
    public AudioClip merchantAllianceStart;
    public AudioClip merchantAllianceComplete;

    // Raid
    public AudioClip raidStart;
    public AudioClip raidComplete;

    private void Awake()
    {
        ui = GetComponent<PlayerUIManager>();
        quests = GetComponent<PlayerQuestTracker>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        animator = ui.announcements.GetComponent<Animator>();
    }

/*    private void OnGUI()
    {
        if (GUILayout.Button("goThroughAnnouncements"))
        {
            goThroughAnnouncementsQueued();
        }
    }*/

    private void goThroughAnnouncementsQueued()
    {
        if(announcementsQueued.Count != 0)
        {
            if (isAnnouncing == false)
            {
                StartCoroutine(announce());
            }
        }
    }

    private IEnumerator announce()
    {
        isAnnouncing = true;

        ui.announcementTopText.GetComponent<TextMeshProUGUI>().text = announcementsQueued[0].topText;
        ui.announcementBottomText.GetComponent<TextMeshProUGUI>().text = announcementsQueued[0].bottomText;
        playSound(announcementsQueued[0].bannerType, announcementsQueued[0].quest.status);

        animator.SetBool("enable", true);

        yield return new WaitForSeconds(5f);

        animator.SetBool("enable", false);

        yield return new WaitForSeconds(2f);

        ui.announcementTopText.GetComponent<TextMeshProUGUI>().text = "";
        ui.announcementBottomText.GetComponent<TextMeshProUGUI>().text = "";

        isAnnouncing = false;

        announcementsQueued.RemoveAt(0);
        goThroughAnnouncementsQueued();
    }

    public void addAnnouncementQueued(string topText, string bottomText, questBannerType type, Quest quest)
    {
        announcementsQueued.Add(new Announcement(topText, bottomText, type, quest));

        goThroughAnnouncementsQueued();
    }

    private void playSound(questBannerType bannerType, QuestStatus status)
    {
        if(bannerType == questBannerType.MerchantAlliance)
        {
            if(status == QuestStatus.inProgress)
            {
                audioSource.clip = merchantAllianceStart;
                audioSource.Play();
            }

            if (status == QuestStatus.completed)
            {
                audioSource.clip = merchantAllianceComplete;
                audioSource.Play();
            }
        }

        if (bannerType == questBannerType.Raid)
        {
            if (status == QuestStatus.inProgress)
            {
                audioSource.clip = raidStart;
                audioSource.Play();
            }

            if (status == QuestStatus.completed)
            {
                audioSource.clip = raidComplete;
                audioSource.Play();
            }
        }
    }

    [System.Serializable]
    public struct Announcement
    {
        public string topText;
        public string bottomText;
        public questBannerType bannerType;
        public Quest quest;

        public Announcement(string topText, string bottomText, questBannerType bannerType, Quest quest)
        {
            this.topText = topText;
            this.bottomText = bottomText;
            this.bannerType = bannerType;
            this.quest = quest;

        }
    }

    public enum questBannerType
    {
        MerchantAlliance,
        Raid
    }
}
