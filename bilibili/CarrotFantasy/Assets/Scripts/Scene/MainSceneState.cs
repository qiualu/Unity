using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneState : BaseSceneState
{
    public MainSceneState(UIFacade uiFacade) : base(uiFacade)
    {
    }

    public override void EnterScene()
    {
        mUIFacade.AddPanelToDict(StringManager.MainPanel);
        mUIFacade.AddPanelToDict(StringManager.SetPanel);
        mUIFacade.AddPanelToDict(StringManager.HelpPanel);
        mUIFacade.AddPanelToDict(StringManager.GameLoadPanel);

        mUIFacade.AddPanelToDict(资源名字.主面板);
        mUIFacade.AddPanelToDict(资源名字.设置面板);
        mUIFacade.AddPanelToDict(资源名字.帮助面板);
        mUIFacade.AddPanelToDict(资源名字.游戏加载面板);

        base.EnterScene();
        GameManager.Instance.audioSourceManager.PlayBGMusic(GameManager.Instance.factoryManager.audioClipFactory.GetSingleResources("Main/BGMusic"));
    }

    public override void ExitScene()
    {
        base.ExitScene();
        //当前对象的类 类型
        if (mUIFacade.currentSceneState.GetType()==typeof(NormalGameOptionSceneState))
        {
            SceneManager.LoadScene(2);
        }
        else if (mUIFacade.currentSceneState.GetType()==typeof(BossGameOptionSceneState))
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }
}
