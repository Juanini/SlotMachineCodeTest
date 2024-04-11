using System;
using System.Collections.Generic;

[Serializable]
public class ReelStripsDataResult
{
    public string[][] ReelStrips;
}

[Serializable]
public class ReelStrip
{
    public string[] symbols;
}

[Serializable]
public class ReelStripsContainer
{
    public ReelStrip[] ReelStrips;
}
