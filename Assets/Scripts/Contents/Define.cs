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
        MaxCount,
    
    }

    public enum EnemyType { 
        Flying,
        Walking,
        C,
    }

    public enum EnemyState
    {
        None,
        Idle,
        Moving,
        Attack,
        Die,
    }
}
