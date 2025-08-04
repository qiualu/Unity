using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class 保卫土豆场景 : BaseSceneState
{
    /// <summary>
    /// 构造函数 
    /// </summary>
    /// <param name="uiFacade">UI外观管理器引用，用于管理面板和场景状态</param>
    public 保卫土豆场景(UIFacade uiFacade) : base(uiFacade)
    {
    }
    /// <summary>
    /// 进入场景时的初始化操作（重写自BaseSceneState）   StringManager / 字符串管理，负责存贮比较常用的字符串，以防止写错
    /// 加载并注册当前场景所需的所有面板    
    /// </summary>
    public override void EnterScene()
    {
        // 向面板字典中添加普通游戏选项面板
        mUIFacade.AddPanelToDict(资源名字.普通游戏选项面板); // 原：StringManager.GameNormalOptionPanel
                                                 // 添加普通游戏大关卡面板
        mUIFacade.AddPanelToDict(资源名字.普通游戏大关卡面板); // 原：StringManager.GameNormalBigLevelPanel
                                                  // 添加普通游戏小关卡面板
        mUIFacade.AddPanelToDict(资源名字.普通游戏小关卡面板); // 原：StringManager.GameNormalLevelPanel
                                                  // 添加帮助面板
        mUIFacade.AddPanelToDict(资源名字.帮助面板); // 原：StringManager.HelpPanel
                                             // 添加游戏加载面板
        mUIFacade.AddPanelToDict(资源名字.游戏加载面板); // 原：StringManager.GameLoadPanel
                                               // 调用父类的进入场景方法（可能包含通用初始化逻辑）
        base.EnterScene();
    }

    /// <summary>
    /// 退出场景时的清理与切换操作（重写自BaseSceneState）
    /// 根据当前面板状态决定加载的目标场景
    /// </summary>
    public override void ExitScene()
    {
        // 从面板字典中获取普通游戏选项面板，并转换为具体类型  gameNormalOptionPanel
        //GameNormalOptionPanel gameNormalOptionPanel = mUIFacade.currentScenePanelDict[StringManager.GameNormalOptionPanel] as GameNormalOptionPanel;
        普通游戏选项面板 普通游戏选项面板实例 = mUIFacade.currentScenePanelDict[资源名字.普通游戏选项面板] as 普通游戏选项面板;

        Debug.Log($"保卫土豆场景 : 退出场景时的清理与切换操作 isInBigLevelPanel:{普通游戏选项面板实例.isInBigLevelPanel} ");

        // 根据面板状态判断加载哪个场景   此处为真 
        if (普通游戏选项面板实例.isInBigLevelPanel)  
        {
            // 如果当前在大关卡面板，加载场景索引为1的场景（可能是主界面）
            SceneManager.LoadScene(1);
        }
        else
        {
            // 否则加载场景索引为3的场景（可能是游戏战斗场景）
            SceneManager.LoadScene(3);
        }

        // 重置大关卡面板状态标记（退出时默认回到大关卡面板状态）
        普通游戏选项面板实例.isInBigLevelPanel = true;

        // 调用父类的退出场景方法（可能包含通用清理逻辑）
        base.ExitScene();
    }


}
