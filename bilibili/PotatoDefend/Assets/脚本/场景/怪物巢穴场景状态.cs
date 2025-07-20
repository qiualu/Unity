using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 怪物窝场景状态
/// </summary>
// public class MonsterNestSceneState : BaseSceneState
public class 怪物巢穴场景状态 : 基于基础场景状态 // MonsterNestSceneState : BaseSceneState
{
    // 构造函数（接收界面外观实例）
    // public MonsterNestSceneState(UIFacade uiFacade) : base(uiFacade)
    public 怪物巢穴场景状态(界面外观 界面外观实例) : base(界面外观实例) // MonsterNestSceneState(UIFacade uiFacade) : base(uiFacade)
    {
    }

    // 进入场景
    // public override void EnterScene()
    public override void 进入场景() // EnterScene()
    {
        // mUIFacade.AddPanelToDict(StringManager.GameLoadPanel);
        界面外观实例.向字典添加面板(字符串管理.游戏加载面板); // mUIFacade.AddPanelToDict(StringManager.GameLoadPanel);
        // mUIFacade.AddPanelToDict(StringManager.MonsterNestPanel);
        界面外观实例.向字典添加面板(字符串管理.怪物巢穴面板); // mUIFacade.AddPanelToDict(StringManager.MonsterNestPanel);
        // base.EnterScene();
        base.进入场景(); // base.EnterScene();

        // 播放怪物巢穴场景背景音乐
        // GameManager.Instance.audioSourceManager.PlayBGMusic(GameManager.Instance.factoryManager.audioClipFactory.GetSingleResources("MonsterNest/BGMusic"));
        游戏管理.实例.音频源管理.播放背景音乐(
        游戏管理.实例.工厂管理.音频片段工厂实例.获取单个资源("MonsterNest/BGMusic")
        ); // GameManager.Instance.audioSourceManager.PlayBGMusic(GameManager.Instance.factoryManager.audioClipFactory.GetSingleResources("MonsterNest/BGMusic"));
    }

    // 退出场景
    // public override void ExitScene()
    public override void 退出场景() // ExitScene()
    {
        // SceneManager.LoadScene(1);
        SceneManager.LoadScene(1); // SceneManager.LoadScene(1);
        // base.ExitScene();
        base.退出场景(); // base.ExitScene();
    }
}