using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : StateMachine<GameStates>
{
    
}

public enum GameStates
{
    Idle = 0,
    DataSetup,
    Win
}