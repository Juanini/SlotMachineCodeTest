using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReelPieceTypeRegistry", menuName = "SO/Types", order = 0)]
public class ReelPieceTypeRegistry : ScriptableObject
{
    public List<ReelPieceType> allSymbolTypes;
    private Dictionary<string, ReelPieceType> symbolTypeByName;

    public void OnEnable() 
    {
        CreateReferences();
    }

    private void CreateReferences()
    {
        if (symbolTypeByName != null) { return; }
        
        symbolTypeByName = new Dictionary<string, ReelPieceType>();
        
        foreach (var symbolType in allSymbolTypes) 
        {
            // symbolTypeByName[symbolType.key.ToUpper()] = symbolType;
            symbolTypeByName.Add(symbolType.key.ToUpper(), symbolType);
        }
    }

    public ReelPieceType GetSymbolTypeByName(string _key) 
    {
        if (symbolTypeByName == null) { CreateReferences(); }
        
        ReelPieceType symbolType = null;
        symbolTypeByName.TryGetValue(_key.ToUpper(), out symbolType);
        return symbolType;
    }
}