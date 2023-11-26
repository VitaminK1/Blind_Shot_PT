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

    public enum Scene { 
        Unknown,
        TutorialScene,
        InGameScene,
        GameOverScene,
        EndingScene,

    }

    public enum Sound {
        Bgm,
        Player,
        MaxCount,
    
    }

    public enum EnemyType { 
        A,
        B,
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
