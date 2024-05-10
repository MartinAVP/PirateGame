using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class playerMoneyManager : MonoBehaviour
{
    public PlayerUIManager ui;
    [SerializeField] private List<moneyObject> moneyQueued;
    [SerializeField] private GameObject moneyCardPrefab;

    public int money;

    private AudioSource audioSource;

    private bool isAnnouncing = false;

    [Header("Audio Clips")]
    public AudioClip moneySound;

    private void Awake()
    {
        ui = this.transform.parent.GetComponent<PlayerUIManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = moneySound;
        money = 0;
    }

/*    private void OnGUI()
    {
        if (GUILayout.Button("Add 20"))
            addMoney(20);
        if (GUILayout.Button("Add 40"))
            addMoney(40);
        if (GUILayout.Button("Add 60"))
            addMoney(60);
        if (GUILayout.Button("Add 10"))
            addMoney(10);
    }*/

    private void goThroughAnnouncementsQueued()
    {
        if (moneyQueued.Count != 0)
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

        yield return new WaitForSeconds(5f);

        isAnnouncing = false;

        moneyQueued.RemoveAt(0);
        goThroughAnnouncementsQueued();
    }

    public void addMoney(int quantity)
    {
        GameObject tempMoney;
        tempMoney = Instantiate(moneyCardPrefab);
        tempMoney.transform.parent = ui.mainMoneyLogContent;
        tempMoney.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+ " + quantity.ToString();
        audioSource.Play();

        moneyQueued.Add(new moneyObject(tempMoney, quantity));

        money += quantity;

        goThroughAnnouncementsQueued();
    }

    [System.Serializable]
    private struct moneyObject
    {
        public GameObject moneyUI;
        public int quantity;

        public moneyObject(GameObject moneyUI, int quantity)
        {
            this.moneyUI = moneyUI;
            this.quantity = quantity;
        }
    }

    public enum questBannerType
    {
        MerchantAlliance,
        Raid
    }
}
