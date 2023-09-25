using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSurce;
    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        audioSurce = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnprogressChangedEventArgs e)
    {
        // Burning Alarm
        float burnShowProgressAmount = 0.5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangeEvetArgs e)
    {
        // Cooking Sound
        bool playSound = e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying;
        if (playSound)
        {
            audioSurce.Play();
        }
        else
        {
            audioSurce.Pause();
        }
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer < 0)
            {
                float warningSoundTimerMax = 0.2f;
                warningSoundTimer = warningSoundTimerMax;
                
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}