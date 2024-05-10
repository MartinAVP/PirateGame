using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    private PlayerUIManager ui;
    private Animator blackScreenAnimator;
    public string currentName;
    public int score;

    public GameObject mainCam;

    private PlayerInventoryManager inventoryManager;
    private playerMoneyManager playerMoneyManager;

    public Animator CreditsAnimator;

    [Header("Score Values")]
    public int bananaValue;
    public int cannonBallValue;

    [Header("High Scores")]
    public GameObject scorePrefab;
    public Transform scoreContent;

    public GameObject highScoreInput;

    [Header("Enemies")]
    public Animator enemyShips;

    private void Awake()
    {
        ui = FindFirstObjectByType<PlayerUIManager>();

        inventoryManager = FindFirstObjectByType<PlayerInventoryManager>();
        playerMoneyManager = FindFirstObjectByType<playerMoneyManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        blackScreenAnimator = ui.blackScreen.GetComponent<Animator>();
        getSavedScores();
    }

    private void sumUpScore()
    {
        score = playerMoneyManager.money;

        foreach(InventoryItem item in inventoryManager.playerInventory)
        {
            if(item.type == ItemType.Banana)
            {
                score += item.quantity * bananaValue;
            }
            else if(item.type == ItemType.CannonBall)
            {
                score += item.quantity * cannonBallValue;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void submitScore()
    {
        if (string.IsNullOrWhiteSpace(currentName))
        {
            print("Name is Empty");
        }
        else
        {
            setScore(score, currentName);
            highScoreInput.SetActive(false);
        }
    }

    public void EndScene()
    {
        ui.blackScreen.gameObject.SetActive(true);

        StartCoroutine(triggerEnding());
    }

    private IEnumerator triggerEnding()
    {
        blackScreenAnimator.SetTrigger("Fade");

        yield return new WaitForSeconds(2f);

        doBehindScene();

        yield return new WaitForSeconds(5f);

        blackScreenAnimator.SetBool("FadeOut", true);
        CreditsAnimator.gameObject.SetActive(true);
        CreditsAnimator.SetTrigger("Roll");
        yield return new WaitForSeconds(18f);
        ui.EnableCursor();
    }

    public void riseShips()
    {
        enemyShips.SetTrigger("rise");
    }

    private void doBehindScene()
    {
        GameObject player = GameObject.Find("Player");
        GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);

        ui.mainLayout.gameObject.SetActive(false);

        this.GetComponent<AudioSource>().Play();

        sumUpScore();
        displayScores();

        FindFirstObjectByType<playerDeath>().respawn();

        player.SetActive(false);
        mainCam.SetActive(true);

    }

    private void OnGUI()
    {
        if(GUILayout.Button("End Scene"))
        {
            EndScene();
        }

        if (GUILayout.Button("Display Scores"))
        {
            displayScores();
        }

        if (GUILayout.Button("InsertScore"))
        {
            setScore(50, "Martin");
        }

        if (GUILayout.Button("InsertScore"))
        {
            setScore(20, "Eric");
        }

        if (GUILayout.Button("InsertScore"))
        {
            setScore(70, "Veronica");
        }

        if (GUILayout.Button("Reset Scores"))
        {
            cleanScores();
        }
    }

    // Scoreding

    scores[] highScores = new scores[10];

    private void getSavedScores()
    {
        for(int i = 0; i < highScores.Length; i++)
        {
            highScores[i].score = PlayerPrefs.GetInt("highScore" + i);
            highScores[i].name = PlayerPrefs.GetString("highScoreName" + i);
        }
    }

    private void cleanScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            PlayerPrefs.SetInt("highScore" + i, 0);
            PlayerPrefs.SetString("highScoreName" + i, "---");
        }

        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i].score = PlayerPrefs.GetInt("highScore" + i);
            highScores[i].name = PlayerPrefs.GetString("highScoreName" + i);
        }

        updateScores();
    }

    GameObject[] scoresDisplayed = new GameObject[10];

    private void displayScores()
    {
        GameObject tmp;
        for (int i = 0; i < highScores.Length; i++)
        {
            tmp = Instantiate(scorePrefab);
            tmp.transform.parent = scoreContent;
            tmp.transform.localScale = Vector3.one;
            tmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString("00");
            tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = highScores[i].score.ToString();
            tmp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = highScores[i].name;
            scoresDisplayed[i] = tmp;
        }
    }

    private void updateScores()
    {
        for (int i = 0;i < highScores.Length; i++)
        {
            scoresDisplayed[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString("00");
            scoresDisplayed[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = highScores[i].score.ToString();
            scoresDisplayed[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = highScores[i].name;
        }
    }

    private void setScore(int newScore, string name)
    {
        bool scoreInserted = false;

        for (int i = 0; i < highScores.Length; i++)
        {
            if (newScore > highScores[i].score)
            {
                for (int j = highScores.Length - 1; j > i; j--)
                {
                    highScores[j] = highScores[j - 1];
                    PlayerPrefs.SetInt("highScore" + i, highScores[j].score);
                    PlayerPrefs.SetString("highScoreName" + i, highScores[j].name);
                }

                highScores[i].score = newScore;
                highScores[i].name = name;

                PlayerPrefs.SetInt("highScore" + i, newScore);
                PlayerPrefs.SetString("highScoreName" + i, name);

                scoreInserted = true;
                break;
            }
        }

        if (!scoreInserted)
        {
            Debug.Log("The score couldn't make it into the high scores.");
        }

        updateScores();
    }

    public void changeCurrentName(string s)
    {
        currentName = s;
    }

    public struct scores
    {
        public int score;
        public string name;
    }
}
