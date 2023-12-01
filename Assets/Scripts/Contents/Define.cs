using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{

    public enum WorldObject 
    { 
        Unknown,
        Player,
        Enemy,

    }

    public enum GameState {
        None,
        InGame,
        GameOver,
        Ending,
    }

    public enum Sound {
        Bgm,
        Player,
        MaxCount,
    
    }

    public enum EnemyType { 
        Flying,
        Walking,
        C,
    }

    public enum State
    {
        Idle,
        Moving,
        Attack,
        Die,

    }


}
