using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class ServerDataLoader : MonoBehaviour
{
    public SpinData GetSpinData()
    {
        SpinData loadedData = JsonUtility.FromJson<SpinData>(ServerConnectionManager.Ins.serverEndpoints.spinsJson.text);
        return loadedData;
    }
    
    // public ReelStripsContainer GetReelsData()
    // {
    //     ReelStripsContainer loadedData = JsonUtility.FromJson<ReelStripsContainer>(ServerConnectionManager.Ins.serverEndpoints.reelsJson.text);
    //     // ReelStripsDataResult data = JsonConvert.DeserializeObject<ReelStripsDataResult>(ServerConnectionManager.Ins.serverEndpoints.reelsJson.text);
    //     
    //     Debug.Log("reelsData " + (loadedData == null ? "null" : "no Null"));
    //     Debug.Log("reelsData loadedData.ReelStrips[0].Length: " + loadedData.ReelStrips.Length);
    //
    //     LoadJsonData(ServerConnectionManager.Ins.serverEndpoints.reelsJson);
    //         
    //     return loadedData;
    // }
    
    void LoadJsonData(TextAsset jsonFile)
    {
        if (jsonFile != null)
        {
            var json = JSON.Parse(jsonFile.text);
            // Ejemplo de acceso: imprimir el primer símbolo de la primera tira
            
            Debug.Log(json["ReelStrips"][0][0]);
        }
        else
        {
            Debug.LogError("No se ha asignado el archivo JSON en el Inspector.");
        }
    }
    
    public List<List<string>> GetReelsData()
    {
        List<List<string>> reelStripsList = new List<List<string>>();
        
        var json = JSON.Parse(ServerConnectionManager.Ins.serverEndpoints.reelsJson.text);
        var reelStrips = json["ReelStrips"];

        for (int i = 0; i < reelStrips.Count; i++)
        {
            List<string> strip = new List<string>();
            for (int j = 0; j < reelStrips[i].Count; j++)
            {
                strip.Add(reelStrips[i][j]);
            }
            reelStripsList.Add(strip);
        }

        return reelStripsList;
    }
}
