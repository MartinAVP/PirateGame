using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHitRegister : MonoBehaviour
{
    [SerializeField] private float timeBetweenHitSound = 90;
    [SerializeField] private float currentTime;

    [SerializeField]private AudioSource[] cannonTargetHitSounds = new AudioSource[8];
    private List<GameObject> activeCannonBalls;
    private int currentSoundID;

    private bool timerRunning = true;

    private void Start()
    {
        currentTime = timeBetweenHitSound;
    }

    private void Update()
    {
        if(timerRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                StopTimer();
            }
        }
    }

    private void newCannonBall(GameObject cannonBall)
    {
        activeCannonBalls.Add(cannonBall);
    }

    private void StopTimer()
    {
        // Stop the Timer
        timerRunning = false;
        currentTime = 0;

        // Set the Hit sound ID to 0
        currentSoundID = 0;
        Debug.Log("Took too long to hit");
    }

    private void StartTimer()
    {
        if (timerRunning == true)
        {
            // Restart the Timer
            timerRunning=true;
            StartTimer();
            Debug.Log("Time Already Running");
        }
        else
        {
            timerRunning=true;
            currentTime = timeBetweenHitSound;
        }
    }

    public void cannonBallHit()
    {

    }

    public void cannonBallHitTarget()
    {
        // Start The Timer for Hitting Sound
        StartTimer();

        // Play the Corresponding Sound
        switch (currentSoundID)
        {
            case 0:
                cannonTargetHitSounds[0].Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 1:
                cannonTargetHitSounds[1].Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 2:
                cannonTargetHitSounds[2].Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 3:
                cannonTargetHitSounds[3].Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 4:
                cannonTargetHitSounds[4].Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 5:
                cannonTargetHitSounds[5].Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 6:
                cannonTargetHitSounds[6].Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 7:
                cannonTargetHitSounds[7].Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID = 0;
                break;
        }
    }

}
