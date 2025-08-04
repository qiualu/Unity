using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通模式关卡选择的共用面板控制器
/// 负责关卡选择界面的返回逻辑和帮助面板切换
/// </summary>
public class 普通游戏选项面板 : BasePanel
{


    /// <summary>
    /// 标记当前是否在大关卡选择面板
    /// true：处于大关卡面板；false：处于小关卡面板
    /// [HideInInspector] 表示不在Inspector面板中显示该字段
    /// </summary>
    [HideInInspector]
    public bool isInBigLevelPanel = true;

    /// <summary>
    /// 返回上一级面板的按钮点击事件
    /// 根据当前面板层级（大/小关卡）决定返回目标 
    /// </summary>
    /// 


    public void 返回按钮()
    {
        Debug.Log("返回按钮");

        if (isInBigLevelPanel)
        {
            // 如果当前在大关卡面板，切换场景状态到主界面场景
            mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade));
        }
        else
        {
            // 如果当前在小关卡面板，退出小关卡面板并显示大关卡面板
            //mUIFacade.currentScenePanelDict[StringManager.GameNormalLevelPanel].ExitPanel();
            //mUIFacade.currentScenePanelDict[StringManager.GameNormalBigLevelPanel].EnterPanel();
            // 如果当前在小关卡面板，退出小关卡面板并显示大关卡面板
            mUIFacade.currentScenePanelDict[资源名字.普通游戏小关卡面板].ExitPanel();
            mUIFacade.currentScenePanelDict[资源名字.普通游戏大关卡面板].EnterPanel();
        }
        // 播放按钮点击音效
        mUIFacade.PlayButtonAudioClip();
        // 重置为大关卡面板状态（无论从哪返回，默认回到大关卡层级）
        isInBigLevelPanel = true;
         
    }

    public void 帮助按钮()
    {
        Debug.Log("帮助按钮");
        // 播放按钮点击音效
        mUIFacade.PlayButtonAudioClip();
        // 显示帮助面板
        mUIFacade.currentScenePanelDict[资源名字.帮助面板].EnterPanel();
    }


        public void ReturnToLastPanel()  // 用到了 返回键
    {
        if (isInBigLevelPanel)
        {
            // 如果当前在大关卡面板，切换场景状态到主界面场景
            mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade));
        }
        else
        {
            // 如果当前在小关卡面板，退出小关卡面板并显示大关卡面板
            //mUIFacade.currentScenePanelDict[StringManager.GameNormalLevelPanel].ExitPanel();
            //mUIFacade.currentScenePanelDict[StringManager.GameNormalBigLevelPanel].EnterPanel();
            // 如果当前在小关卡面板，退出小关卡面板并显示大关卡面板
            mUIFacade.currentScenePanelDict[资源名字.普通游戏小关卡面板].ExitPanel();
            mUIFacade.currentScenePanelDict[资源名字.普通游戏大关卡面板].EnterPanel();
        }
        // 播放按钮点击音效
        mUIFacade.PlayButtonAudioClip();
        // 重置为大关卡面板状态（无论从哪返回，默认回到大关卡层级）
        isInBigLevelPanel = true;
    }

    /// <summary>
    /// 打开帮助面板的按钮点击事件
    /// </summary>
    public void ToHelpPanel()
    {
        // 播放按钮点击音效
        mUIFacade.PlayButtonAudioClip();
        // 显示帮助面板
        mUIFacade.currentScenePanelDict[资源名字.帮助面板].EnterPanel();
    }




}












