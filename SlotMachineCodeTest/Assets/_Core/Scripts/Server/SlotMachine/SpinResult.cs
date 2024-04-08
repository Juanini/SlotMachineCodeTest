
using System.Collections.Generic;

[System.Serializable]
public class SpinResult
{
    public List<int> ReelIndex;
    public int ActiveReelCount;
    public int WinAmount;
}

[System.Serializable]
public class SpinData
{
    public List<SpinResult> Spins;
}