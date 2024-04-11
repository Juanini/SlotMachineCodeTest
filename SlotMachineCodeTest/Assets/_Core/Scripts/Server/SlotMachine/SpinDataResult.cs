
using System.Collections.Generic;

[System.Serializable]
public class SpinDataResult
{
    public List<int> ReelIndex;
    public int ActiveReelCount;
    public int WinAmount;
}

[System.Serializable]
public class SpinData
{
    public List<SpinDataResult> Spins;
}