using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Obvious.Soap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineMenu : MonoBehaviour
{
    public TextMeshProUGUI totalWinText;
    public Button spinButton;

    [Header("Properties")] 
    public float textAnimTime = 1.2f;
    
    [Header("Events")]
    public ScriptableEventNoParam OnSpinStart;
    public ScriptableEvent<int> OnSpinCompleted;

    private void Start()
    {
        OnSpinStart.OnRaised += onSpinStart;
        OnSpinCompleted.OnRaised += onSpinCompleted;
    }
    
    private void onSpinCompleted(int _winAmount)
    {
        DOTween.To(() => 0, x => totalWinText.text = $"${x:0}", _winAmount, textAnimTime)
            .OnComplete(TextAnimComplete);
    }

    private void TextAnimComplete()
    {
        spinButton.interactable = true;
    }

    private void onSpinStart()
    {
        totalWinText.text = 0.ToString();
        spinButton.interactable = false;
    }

    private void OnDestroy()
    {
        OnSpinStart.OnRaised += onSpinStart;
    }
}
