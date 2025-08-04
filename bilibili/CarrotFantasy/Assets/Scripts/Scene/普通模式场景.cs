using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 普通模式场景 : BaseSceneState
{
    public 普通模式场景(UIFacade uiFacade) : base(uiFacade)
    {
    }

    public override void EnterScene()
    {
        //mUIFacade.AddPanelToDict(StringManager.GameLoadPanel);
        //mUIFacade.AddPanelToDict(StringManager.NormalModelPanel);
        mUIFacade.AddPanelToDict(资源名字.游戏加载面板);
        mUIFacade.AddPanelToDict(资源名字.普通模式面板);
        base.EnterScene();
        GameManager.Instance.audioSourceManager.CloseBGMusic();
    }

    public override void ExitScene()
    {
        base.ExitScene();
        GameManager.Instance.audioSourceManager.OpenBGMusic();
    }
}
