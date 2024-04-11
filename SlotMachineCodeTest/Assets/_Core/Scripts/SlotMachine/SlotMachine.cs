using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Obvious.Soap;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotMachine : MonoBehaviour
{
    public SpinData spinData;
    public List<List<string>> reelsData;

    public List<Reel> reels;

    private SpinDataResult activeSpinData;

    [Header("Events")] public ScriptableEventNoParam OnSpinStart;
    [Header("Events")] public ScriptableEvent<int> OnSpinCompleted;

    private bool firstSpinDone = false;
    public bool FirstSpinDone => firstSpinDone;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetData();
        SetReels();

        OnSpinStart.OnRaised += StartSpin;
    }

    private void StartSpin()
    {
        PickRandomSpinData();
        Spin();
    }

    private void PickRandomSpinData()
    {
        activeSpinData = spinData.Spins[Random.Range(0, spinData.Spins.Count)];
        PrintActiveSpinData();
    }

    private async void Spin()
    {
        Trace.Log(this.name + " - " + "Spin");

        reelsStopped = 0;

        for (int i = 0; i < reels.Count; i++)
        {
            reels[i].Spin(activeSpinData.ReelIndex[i]);
        }
        
        await UniTask.Delay(1000);
        
        reels[0].InitStop();
    }

    private int reelsStopped = 0;

    public async void OnReelStopped()
    {
        reelsStopped++;
        await UniTask.Delay(300);

        if (reelsStopped >= reels.Count)
        {
            await UniTask.Delay(500);
            firstSpinDone = true;
            OnSpinCompleted.Raise(activeSpinData.WinAmount);
        }
        else
        {
            reels[reelsStopped].InitStop();
        }
    }

    private void SetData()
    {
        spinData = ServerConnectionManager.Ins.serverDataLoader.GetSpinData();
        reelsData = ServerConnectionManager.Ins.serverDataLoader.GetReelsData();
    }

    private void SetReels()
    {
        for (int i = 0; i < reelsData.Count; i++)
        {
            reels[i].Setup(reelsData[i], i, this);
        }
    }

    private void OnDestroy()
    {
        OnSpinStart.OnRaised -= StartSpin;
    }
    
    // * =====================================================================================================================================
    // * DEBUG

    #region DEBUG
    
    private void PrintActiveSpinData()
    {
        Trace.Log("Spin Data Selected: ");
        
        Trace.Log("ActiveReelCount: " + activeSpinData.ActiveReelCount);
        Trace.Log("Reel Index: ");
        foreach (var reelIndex in activeSpinData.ReelIndex)
        {
            Trace.Log(" " + reelIndex);
        }
        Trace.Log("WinAmount: " + activeSpinData.WinAmount);
    }
    
    #endregion
}
