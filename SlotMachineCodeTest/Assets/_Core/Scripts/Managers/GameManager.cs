using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Events")]
    public ScriptableEventNoParam OnGameInitComplete;
    
    void Start()
    {
        Init();       
    }

    private async void Init()
    {
        await ServerConnectionManager.Ins.Init();
        await UI.Ins.Init();

        OnGameInitComplete.Raise();
    }
}
