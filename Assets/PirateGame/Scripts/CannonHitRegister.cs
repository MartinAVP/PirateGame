using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHitRegister : MonoBehaviour
{
    [SerializeField] private float timeBetweenHitSound = 90;
    [SerializeField] private float currentTime;

    [SerializeField]public AudioClip[] cannonTargetHitSounds = new AudioClip[10];
    private int currentSoundID;

    public AudioSource cannonSound;

    private bool timerRunning = true;

    private void Start()
    {
        currentTime = timeBetweenHitSound;
        cannonSound = this.GetComponent<AudioSource>();
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
        timerRunning = true;
        currentTime = timeBetweenHitSound;
    }

    public void cannonBallHit()
    {

    }

    public void cannonBallHitTarget()
    {
        Debug.Log("Cannon Ball hit sound");
        // Start The Timer for Hitting Sound
        StartTimer();

        // Play the Corresponding Sound
        switch (currentSoundID)
        {
            case 0:
                cannonSound.clip = cannonTargetHitSounds[0];
                cannonSound.Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 1:
                cannonSound.clip = cannonTargetHitSounds[1];
                cannonSound.Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 2:
                cannonSound.clip = cannonTargetHitSounds[2];
                cannonSound.Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 3:
                cannonSound.clip = cannonTargetHitSounds[3];
                cannonSound.Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 4:
                cannonSound.clip = cannonTargetHitSounds[4];
                cannonSound.Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 5:
                cannonSound.clip = cannonTargetHitSounds[5];
                cannonSound.Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 6:
                cannonSound.clip = cannonTargetHitSounds[6];
                cannonSound.Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 7:
                cannonSound.clip = cannonTargetHitSounds[7];
                cannonSound.Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 8:
                cannonSound.clip = cannonTargetHitSounds[8];
                cannonSound.Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID++;
                break;
            case 9:
                cannonSound.clip = cannonTargetHitSounds[9];
                cannonSound.Play();
                Debug.Log("Hit: " + currentSoundID);
                currentSoundID = 0;
                break;
        }
    }

}
