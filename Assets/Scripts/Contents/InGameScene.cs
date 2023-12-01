using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : BaseScene
{
    public override void Clear()
    {

    }

    public void StartBGM() {
        Managers.Sound.Play("BGM");
    }
}
