using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : BaseScene
{
    [SerializeField]
    protected int _wave = 0;

    public int Wave
    {
        get { return _wave; }
        set { _wave = Mathf.Max(0, value); }
    
    }

    protected override void Init()
    {
        _wave++;
        base.Init();

        SceneType = Define.Scene.InGameScene;
        Managers.Game.Player = GameObject.FindGameObjectWithTag("Player");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);
    }

    public override void Clear()
    {

    }

    public void StartBGM() {
        Managers.Sound.Play("BGM");
    }
}
