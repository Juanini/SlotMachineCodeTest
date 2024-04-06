using UnityEngine;

namespace Obvious.Soap.Example
{
    [CreateAssetMenu(fileName = "scriptable_variable_" + nameof(Player), menuName = "Soap/ScriptableVariables/"+ nameof(Player))]
    public class PlayerVariable : ScriptableVariable<Player>
    {
        
    }
}
