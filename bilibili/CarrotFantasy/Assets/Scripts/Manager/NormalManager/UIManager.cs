using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理器，负责统筹管理所有UI面板的创建、回收和状态切换
/// 是UI系统的核心控制类，协调UIFacade与游戏管理器的交互
/// </summary>
public class UIManager
{
    /// <summary>
    /// UI外观类实例，负责具体的面板显示逻辑和场景状态管理
    /// </summary>
    public UIFacade mUIFacade;

    /// <summary>
    /// 当前场景中已加载的UI面板字典
    /// 键：面板名称（与资源名字类中的常量对应）
    /// 值：面板的游戏对象
    /// </summary>
    public Dictionary<string, GameObject> currentScenePanelDict;

    /// <summary>
    /// 游戏管理器引用，用于访问对象工厂等核心功能
    /// </summary>
    private GameManager mGameManager;

    /// <summary>
    /// 构造函数，初始化UI管理器
    /// </summary>
    public UIManager()
    {
        // 获取游戏管理器的单例实例
        mGameManager = GameManager.Instance;
        // 初始化当前场景面板字典
        currentScenePanelDict = new Dictionary<string, GameObject>();
        // 创建UI外观类实例，并将自身作为参数传入
        mUIFacade = new UIFacade(this);
        // 初始场景状态设为开始加载场景状态
        mUIFacade.currentSceneState = new StartLoadSceneState(mUIFacade);
    }

    /// <summary>
    /// 将UI面板放回对象工厂（对象池），实现面板的复用
    /// </summary>
    /// <param name="uiPanelName">面板的资源名称（用于工厂识别）</param>
    /// <param name="uiPanelGo">要回收的面板游戏对象</param>
    private void PushUIPanel(string uiPanelName, GameObject uiPanelGo)
    {
        // 调用游戏管理器的方法，将面板放入UI面板工厂
        mGameManager.PushGameObjectToFactory(FactoryType.UIPanelFactory, uiPanelName, uiPanelGo);
    }

    /// <summary>
    /// 清空当前场景的面板字典，并将所有面板回收至对象工厂
    /// 用于场景切换时清理当前场景的UI资源
    /// </summary>
    public void ClearDict()
    {
        // 遍历字典中所有面板，逐一回收
        foreach (var item in currentScenePanelDict)
        {
            // 截取面板名称（去除实例化时自动添加的"(Clone)"后缀）
            // 例如"主面板(Clone)" → 截取为"主面板"
            string panelName = item.Value.name.Substring(0, item.Value.name.Length - 7);
            // 回收面板到工厂
            PushUIPanel(panelName, item.Value);
        }

        // 清空字典，释放引用
        currentScenePanelDict.Clear();
    }
}