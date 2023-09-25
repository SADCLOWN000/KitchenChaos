using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";
    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_FLASHING, false);
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += SotveCounter_OnProgressChanged;
    }

    private void SotveCounter_OnProgressChanged(object sender, IHasProgress.OnprogressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        animator.SetBool(IS_FLASHING, show);
    }
}