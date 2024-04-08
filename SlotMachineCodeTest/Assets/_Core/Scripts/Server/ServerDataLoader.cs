using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerDataLoader : MonoBehaviour
{
    private void Start()
    {
        LoadSpinData(ServerConnectionManager.Ins.serverEndpoints.spinsJson);
    }
    
    public SpinData LoadSpinData(TextAsset jsonFilePath)
    {
        SpinData loadedData = JsonUtility.FromJson<SpinData>(jsonFilePath.text);
        return loadedData;
    }
    
    public SpinData GetReelsData(TextAsset jsonFilePath)
    {
        SpinData loadedData = JsonUtility.FromJson<SpinData>(jsonFilePath.text);
        return loadedData;
    }
}
